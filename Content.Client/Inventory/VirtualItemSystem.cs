// SPDX-FileCopyrightText: 2024 AJCM-git <60196617+AJCM-git@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Client.Hands.UI;
using Content.Client.Items;
using Content.Shared.Inventory.VirtualItem;

namespace Content.Client.Inventory;

public sealed class VirtualItemSystem : SharedVirtualItemSystem
{
    public override void Initialize()
    {
        base.Initialize();

        Subs.ItemStatus<VirtualItemComponent>(_ => new HandVirtualItemStatus());
    }
}
