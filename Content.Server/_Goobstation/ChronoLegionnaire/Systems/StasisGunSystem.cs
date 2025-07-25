// SPDX-FileCopyrightText: 2024 BombasterDS <115770678+BombasterDS@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2024 Tadeo <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later AND MIT

using Content.Server._Goobstation.ChronoLegionnaire.Components;
using Content.Shared._Goobstation.ChronoLegionnaire.Components;
using Content.Shared.Interaction.Events;
using Content.Shared.Inventory;
using Content.Shared.Throwing;

namespace Content.Server._Goobstation.ChronoLegionnaire;

public sealed partial class StasisGunSystem : EntitySystem
{
    [Dependency] private readonly InventorySystem _inventory = default!;
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<StasisGunComponent, DroppedEvent>(OnWeaponDrop);
        SubscribeLocalEvent<StasisGunComponent, ThrownEvent>(OnWeaponThrown);
    }

    /// <summary>
    /// Return weapon on belt when it dropped
    /// </summary>
    private void OnWeaponDrop(Entity<StasisGunComponent> gun, ref DroppedEvent args)
    {
        if (args.Handled)
            return;

        if (!HasComp<StasisImmunityComponent>(args.User))
            return;

        args.Handled = _inventory.TryEquip(args.User, gun, gun.Comp.ReturningSlot, predicted: true, force: true);
    }

    /// <summary>
    /// Return weapon on belt when it thrown
    /// </summary>
    private void OnWeaponThrown(Entity<StasisGunComponent> gun, ref ThrownEvent args)
    {
        if (!HasComp<StasisImmunityComponent>(args.User))
            return;

        _inventory.TryEquip(args.User.Value, gun, gun.Comp.ReturningSlot, predicted: true, force: true);
    }


}
