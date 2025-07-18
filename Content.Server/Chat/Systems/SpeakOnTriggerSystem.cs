// SPDX-FileCopyrightText: 2024 MilenVolf <63782763+MilenVolf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Tadeo <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2025 Tay <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2025 pa.pecherskij <pa.pecherskij@interfax.ru>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Server.Explosion.EntitySystems;
using Content.Shared.Timing;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Server.Chat.Systems;

public sealed class SpeakOnTriggerSystem : EntitySystem
{
    [Dependency] private readonly UseDelaySystem _useDelay = default!;
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
    [Dependency] private readonly ChatSystem _chat = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<SpeakOnTriggerComponent, TriggerEvent>(OnTrigger);
    }

    private void OnTrigger(Entity<SpeakOnTriggerComponent> ent, ref TriggerEvent args)
    {
        TrySpeak(ent);
        args.Handled = true;
    }

    private void TrySpeak(Entity<SpeakOnTriggerComponent> ent)
    {
        // If it doesn't have the use delay component, still send the message.
        if (TryComp<UseDelayComponent>(ent.Owner, out var useDelay) && _useDelay.IsDelayed((ent.Owner, useDelay)))
            return;

        if (!_prototypeManager.TryIndex(ent.Comp.Pack, out var messagePack))
            return;

        var message = Loc.GetString(_random.Pick(messagePack.Values));
        // Chatcode moment: messages starting with "." are considered radio messages.
        // Prepending ">" forces the message to be spoken instead.
        // TODO chat refactor: remove this
        message = '>' + message;
        _chat.TrySendInGameICMessage(ent.Owner, message, InGameICChatType.Speak, true);

        if (useDelay != null)
            _useDelay.TryResetDelay((ent.Owner, useDelay));
    }
}
