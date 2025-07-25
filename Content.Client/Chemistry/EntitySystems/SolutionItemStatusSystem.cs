// SPDX-FileCopyrightText: 2024 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Client.Chemistry.Components;
using Content.Client.Chemistry.UI;
using Content.Client.Items;
using Content.Shared.Chemistry.EntitySystems;

namespace Content.Client.Chemistry.EntitySystems;

/// <summary>
/// Wires up item status logic for <see cref="SolutionItemStatusComponent"/>.
/// </summary>
/// <seealso cref="SolutionStatusControl"/>
public sealed class SolutionItemStatusSystem : EntitySystem
{
    [Dependency] private readonly SharedSolutionContainerSystem _solutionContainerSystem = default!;

    public override void Initialize()
    {
        base.Initialize();
        Subs.ItemStatus<SolutionItemStatusComponent>(
            entity => new SolutionStatusControl(entity, EntityManager, _solutionContainerSystem));
    }
}
