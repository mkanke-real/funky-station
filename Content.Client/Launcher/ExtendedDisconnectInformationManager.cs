// SPDX-FileCopyrightText: 2022 20kdc <asdd2808@gmail.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using System;
using Robust.Client;
using Robust.Client.UserInterface;
using Robust.Shared.IoC;
using Robust.Shared.Log;
using Robust.Shared.Network;

namespace Content.Client.Launcher;

/// <summary>
/// So apparently the way that disconnect information is shipped around is really indirect.
/// But honestly, given that content might have additional flags (i.e. hide disconnect button for bans)?
/// This is responsible for collecting any extended disconnect information.
/// </summary>
public sealed class ExtendedDisconnectInformationManager
{
    [Dependency] private readonly IClientNetManager _clientNetManager = default!;

    private NetDisconnectedArgs? _lastNetDisconnectedArgs = null;

    public NetDisconnectedArgs? LastNetDisconnectedArgs
    {
        get => _lastNetDisconnectedArgs;
        private set
        {
            _lastNetDisconnectedArgs = value;
            LastNetDisconnectedArgsChanged?.Invoke(value);
        }
    }

    // BE CAREFUL!
    // This may fire at an arbitrary time before or after whatever code that needs it.
    public event Action<NetDisconnectedArgs?>? LastNetDisconnectedArgsChanged;

    public void Initialize()
    {
        _clientNetManager.Disconnect += OnNetDisconnect;
    }

    private void OnNetDisconnect(object? sender, NetDisconnectedArgs args)
    {
        LastNetDisconnectedArgs = args;
    }
}

