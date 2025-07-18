// SPDX-FileCopyrightText: 2024 to4no_fix <156101927+chavonadelal@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.Clothing.EntitySystems;
using Robust.Shared.GameStates;

namespace Content.Shared.Clothing.Components;

/// <summary>
///     The component prohibits the player from taking off clothes on them that have this component.
/// </summary>
/// <remarks>
///     See also ClothingComponent.EquipDelay if you want the clothes that the player cannot take off by himself to be put on by the player with a delay.
///</remarks>
[NetworkedComponent]
[RegisterComponent]
[Access(typeof(SelfUnremovableClothingSystem))]
public sealed partial class SelfUnremovableClothingComponent : Component
{

}
