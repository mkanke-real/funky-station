// SPDX-FileCopyrightText: 2022 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 TemporalOroboros <TemporalOroboros@gmail.com>
// SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Client.Inventory;
using Content.Shared.Cuffs.Components;
using Content.Shared.Ensnaring.Components;
using Content.Shared.Hands;
using Content.Shared.Inventory.Events;
using Content.Shared.Strip;
using Content.Shared.Strip.Components;

namespace Content.Client.Strip;

/// <summary>
///     This is the client-side stripping system, which just triggers UI updates on events.
/// </summary>
public sealed class StrippableSystem : SharedStrippableSystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<StrippableComponent, CuffedStateChangeEvent>(OnCuffStateChange);
        SubscribeLocalEvent<StrippableComponent, DidEquipEvent>(UpdateUi);
        SubscribeLocalEvent<StrippableComponent, DidUnequipEvent>(UpdateUi);
        SubscribeLocalEvent<StrippableComponent, DidEquipHandEvent>(UpdateUi);
        SubscribeLocalEvent<StrippableComponent, DidUnequipHandEvent>(UpdateUi);
        SubscribeLocalEvent<StrippableComponent, EnsnaredChangedEvent>(UpdateUi);
    }

    private void OnCuffStateChange(EntityUid uid, StrippableComponent component, ref CuffedStateChangeEvent args)
    {
        UpdateUi(uid, component);
    }

    public void UpdateUi(EntityUid uid, StrippableComponent? component = null, EntityEventArgs? args = null)
    {
        if (!TryComp(uid, out UserInterfaceComponent? uiComp))
            return;

        foreach (var ui in uiComp.ClientOpenInterfaces.Values)
        {
            if (ui is StrippableBoundUserInterface stripUi)
                stripUi.DirtyMenu();
        }
    }
}
