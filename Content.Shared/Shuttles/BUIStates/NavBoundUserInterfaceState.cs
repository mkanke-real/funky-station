// SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.Serialization;

namespace Content.Shared.Shuttles.BUIStates;

/// <summary>
/// Wrapper around <see cref="NavInterfaceState"/>
/// </summary>
[Serializable, NetSerializable]
public sealed class NavBoundUserInterfaceState : BoundUserInterfaceState
{
    public NavInterfaceState State;

    public NavBoundUserInterfaceState(NavInterfaceState state)
    {
        State = state;
    }
}
