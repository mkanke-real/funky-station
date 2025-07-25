// SPDX-FileCopyrightText: 2023 0x6273 <0x40@keemail.me>
// SPDX-FileCopyrightText: 2023 Pieter-Jan Briers <pieterjan.briers@gmail.com>
// SPDX-FileCopyrightText: 2024 Ed <96445749+TheShuEd@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2024 Tadeo <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2024 lzk <124214523+lzk228@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using System.Linq;
using System.Numerics;
using Content.Server.Anomaly.Components;
using Content.Server.DeviceLinking.Systems;
using Content.Server.Power.Components;
using Content.Server.Power.EntitySystems;
using Content.Shared.Anomaly.Components;
using Content.Shared.Examine;
using Content.Shared.Interaction;
using Content.Shared.Popups;
using Content.Shared.Power;
using Robust.Shared.Audio.Systems;
using Content.Shared.Verbs;
using Robust.Shared.Timing;

namespace Content.Server.Anomaly;

/// <summary>
/// a device that allows you to translate anomaly activity into multitool signals.
/// </summary>
public sealed partial class AnomalySynchronizerSystem : EntitySystem
{
    [Dependency] private readonly AnomalySystem _anomaly = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly EntityLookupSystem _entityLookup = default!;
    [Dependency] private readonly DeviceLinkSystem _signal = default!;
    [Dependency] private readonly SharedTransformSystem _transform = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly PowerReceiverSystem _power = default!;
    [Dependency] private readonly IGameTiming _timing = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<AnomalySynchronizerComponent, InteractHandEvent>(OnInteractHand);
        SubscribeLocalEvent<AnomalySynchronizerComponent, PowerChangedEvent>(OnPowerChanged);
        SubscribeLocalEvent<AnomalySynchronizerComponent, ExaminedEvent>(OnExamined);
        SubscribeLocalEvent<AnomalySynchronizerComponent, GetVerbsEvent<InteractionVerb>>(OnGetInteractionVerbs);

        SubscribeLocalEvent<AnomalyPulseEvent>(OnAnomalyPulse);
        SubscribeLocalEvent<AnomalySeverityChangedEvent>(OnAnomalySeverityChanged);
        SubscribeLocalEvent<AnomalyStabilityChangedEvent>(OnAnomalyStabilityChanged);
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<AnomalySynchronizerComponent, TransformComponent>();
        while (query.MoveNext(out var uid, out var sync, out var xform))
        {
            if (sync.ConnectedAnomaly is null)
                continue;

            if (_timing.CurTime < sync.NextCheckTime)
                continue;
            sync.NextCheckTime += sync.CheckFrequency;

            if (Transform(sync.ConnectedAnomaly.Value).MapUid != Transform(uid).MapUid)
            {
                DisconnectFromAnomaly((uid, sync), sync.ConnectedAnomaly.Value);
                continue;
            }

            if (!xform.Coordinates.TryDistance(EntityManager, Transform(sync.ConnectedAnomaly.Value).Coordinates, out var distance))
                continue;

            if (distance > sync.AttachRange)
                DisconnectFromAnomaly((uid, sync), sync.ConnectedAnomaly.Value);
        }
    }

    /// <summary>
    /// If powered, try to attach a nearby anomaly.
    /// </summary>
    public bool TryAttachNearbyAnomaly(Entity<AnomalySynchronizerComponent> ent, EntityUid? user = null)
    {
        if (!_power.IsPowered(ent))
        {
            if (user is not null)
                _popup.PopupEntity(Loc.GetString("base-computer-ui-component-not-powered", ("machine", ent)), ent, user.Value);

            return false;
        }

        var coords = _transform.GetMapCoordinates(ent);
        var anomaly = _entityLookup.GetEntitiesInRange<AnomalyComponent>(coords, ent.Comp.AttachRange).FirstOrDefault();

        if (anomaly.Owner is { Valid: false }) // no anomaly in range
        {
            if (user is not null)
                _popup.PopupEntity(Loc.GetString("anomaly-sync-no-anomaly"), ent, user.Value);

            return false;
        }

        ConnectToAnomaly(ent, anomaly);
        return true;
    }

    private void OnPowerChanged(Entity<AnomalySynchronizerComponent> ent, ref PowerChangedEvent args)
    {
        if (args.Powered)
            return;

        if (ent.Comp.ConnectedAnomaly is null)
            return;

        DisconnectFromAnomaly(ent, ent.Comp.ConnectedAnomaly.Value);
    }

    private void OnExamined(Entity<AnomalySynchronizerComponent> ent, ref ExaminedEvent args)
    {
        args.PushMarkup(Loc.GetString(ent.Comp.ConnectedAnomaly.HasValue ? "anomaly-sync-examine-connected" : "anomaly-sync-examine-not-connected"));
    }

    private void OnGetInteractionVerbs(Entity<AnomalySynchronizerComponent> ent, ref GetVerbsEvent<InteractionVerb> args)
    {
        if (!args.CanAccess || !args.CanInteract || args.Hands is null || ent.Comp.ConnectedAnomaly.HasValue)
            return;

        var user = args.User;
        args.Verbs.Add(new()
        {
            Act = () =>
            {
                TryAttachNearbyAnomaly(ent, user);
            },
            Message = Loc.GetString("anomaly-sync-connect-verb-message", ("machine", ent)),
            Text = Loc.GetString("anomaly-sync-connect-verb-text"),
        });
    }

    private void OnInteractHand(Entity<AnomalySynchronizerComponent> ent, ref InteractHandEvent args)
    {
        TryAttachNearbyAnomaly(ent, args.User);
    }

    private void ConnectToAnomaly(Entity<AnomalySynchronizerComponent> ent, Entity<AnomalyComponent> anomaly)
    {
        if (ent.Comp.ConnectedAnomaly == anomaly)
            return;

        ent.Comp.ConnectedAnomaly = anomaly;
        //move the anomaly to the center of the synchronizer, for aesthetics.
        var targetXform = _transform.GetWorldPosition(ent);
        _transform.SetWorldPosition(anomaly, targetXform);

        if (ent.Comp.PulseOnConnect)
            _anomaly.DoAnomalyPulse(anomaly, anomaly);

        _popup.PopupEntity(Loc.GetString("anomaly-sync-connected"), ent, PopupType.Medium);
        _audio.PlayPvs(ent.Comp.ConnectedSound, ent);
    }

    //TODO: disconnection from the anomaly should also be triggered if the anomaly is far away from the synchronizer.
    //Currently only bluespace anomaly can do this, but for some reason it is the only one that cannot be connected to the synchronizer.
    private void DisconnectFromAnomaly(Entity<AnomalySynchronizerComponent> ent, EntityUid other)
    {
        if (ent.Comp.ConnectedAnomaly == null)
            return;

        if (TryComp<AnomalyComponent>(other, out var anomaly))
        {
            if (ent.Comp.PulseOnDisconnect)
                _anomaly.DoAnomalyPulse(ent.Comp.ConnectedAnomaly.Value, anomaly);
        }

        _popup.PopupEntity(Loc.GetString("anomaly-sync-disconnected"), ent, PopupType.Large);
        _audio.PlayPvs(ent.Comp.ConnectedSound, ent);

        ent.Comp.ConnectedAnomaly = null;
    }

    private void OnAnomalyPulse(ref AnomalyPulseEvent args)
    {
        var query = EntityQueryEnumerator<AnomalySynchronizerComponent>();
        while (query.MoveNext(out var uid, out var component))
        {
            if (args.Anomaly != component.ConnectedAnomaly)
                continue;

            if (!_power.IsPowered(uid))
                continue;

            _signal.InvokePort(uid, component.PulsePort);
        }
    }

    private void OnAnomalySeverityChanged(ref AnomalySeverityChangedEvent args)
    {
        var query = EntityQueryEnumerator<AnomalySynchronizerComponent>();
        while (query.MoveNext(out var ent, out var component))
        {
            if (args.Anomaly != component.ConnectedAnomaly)
                continue;

            if (!_power.IsPowered(ent))
                continue;

            //The superscritical port is invoked not at the AnomalySupercriticalEvent,
            //but at the moment the growth animation starts. Otherwise, there is no point in this port.
            //ATTENTION! the console command supercriticalanomaly does not work here,
            //as it forcefully causes growth to start without increasing severity.
            if (args.Severity >= 1)
                _signal.InvokePort(ent, component.SupercritPort);
        }
    }

    private void OnAnomalyStabilityChanged(ref AnomalyStabilityChangedEvent args)
    {
        Entity<AnomalyComponent> anomaly = (args.Anomaly, Comp<AnomalyComponent>(args.Anomaly));

        var query = EntityQueryEnumerator<AnomalySynchronizerComponent>();
        while (query.MoveNext(out var ent, out var component))
        {
            if (component.ConnectedAnomaly != anomaly)
                continue;

            if (!_power.IsPowered(ent))
                continue;

            if (args.Stability < anomaly.Comp.DecayThreshold)
            {
                _signal.InvokePort(ent, component.DecayingPort);
            }
            else if (args.Stability > anomaly.Comp.GrowthThreshold)
            {
                _signal.InvokePort(ent, component.GrowingPort);
            }
            else
            {
                _signal.InvokePort(ent, component.StabilizePort);
            }
        }
    }
}
