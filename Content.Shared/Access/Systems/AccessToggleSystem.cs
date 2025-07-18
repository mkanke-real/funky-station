// SPDX-FileCopyrightText: 2024 deltanedas <39013340+deltanedas@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.Access.Components;
using Content.Shared.Item.ItemToggle.Components;

namespace Content.Shared.Access.Systems;

public sealed class AccessToggleSystem : EntitySystem
{
    [Dependency] private readonly SharedAccessSystem _access = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<AccessToggleComponent, ItemToggledEvent>(OnToggled);
    }

    private void OnToggled(Entity<AccessToggleComponent> ent, ref ItemToggledEvent args)
    {
        _access.SetAccessEnabled(ent, args.Activated);
    }
}
