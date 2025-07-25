// SPDX-FileCopyrightText: 2024 Tayrtahn <tayrtahn@gmail.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

namespace Content.Server.Wires;

/// <summary>
/// Picks a random wire on the entity's <see cref="WireComponent"/> and cuts it.
/// Runs at MapInit and removes itself afterwards.
/// </summary>
[RegisterComponent]
public sealed partial class CutWireOnMapInitComponent : Component
{
}
