// SPDX-FileCopyrightText: 2024 deltanedas <39013340+deltanedas@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.Item.ItemToggle;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared.Item.ItemToggle.Components;

/// <summary>
/// Adds or removes components when toggled.
/// Requires <see cref="ItemToggleComponent"/>.
/// </summary>
[RegisterComponent, NetworkedComponent, Access(typeof(ComponentTogglerSystem))]
public sealed partial class ComponentTogglerComponent : Component
{
    /// <summary>
    /// The components to add when activated.
    /// </summary>
    [DataField(required: true)]
    public ComponentRegistry Components = new();

    /// <summary>
    /// The components to remove when deactivated.
    /// If this is null <see cref="Components"/> is reused.
    /// </summary>
    [DataField]
    public ComponentRegistry? RemoveComponents;

    /// <summary>
    /// If true, adds components on the entity's parent instead of the entity itself.
    /// </summary>
    [DataField]
    public bool Parent;
}
