// SPDX-FileCopyrightText: 2024 Tadeo <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later AND MIT

using Content.Shared.Body.Organ;
namespace Content.Shared._Shitmed.Body.Organ;

public readonly record struct OrganComponentsModifyEvent(EntityUid Body, bool Add);

[ByRefEvent]
public readonly record struct OrganEnableChangedEvent(bool Enabled);

[ByRefEvent]
public readonly record struct OrganEnabledEvent(Entity<OrganComponent> Organ);

[ByRefEvent]
public readonly record struct OrganDisabledEvent(Entity<OrganComponent> Organ);
