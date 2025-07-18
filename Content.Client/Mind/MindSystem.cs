// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.Mind;

namespace Content.Client.Mind;

public sealed class MindSystem : SharedMindSystem
{
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<MindComponent, AfterAutoHandleStateEvent>(OnHandleState);
    }

    private void OnHandleState(EntityUid uid, MindComponent component, ref AfterAutoHandleStateEvent args)
    {
        // Because minds are generally not networked, there might be weird situations were a client thinks multiple
        // users share a mind? E.g., if an admin periodical gets sent all minds via some PVS override, but doesn't get
        // sent intermediate states? Not sure if this is actually possible, but better to be safe.
        foreach (var (user, mind) in UserMinds)
        {
            if (mind == uid)
                UserMinds.Remove(user);
        }

        if (component.UserId != null)
            UserMinds[component.UserId.Value] = uid;
    }
}
