// SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2024 Tadeo <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2024 username <113782077+whateverusername0@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 whateverusername0 <whateveremail>
// SPDX-FileCopyrightText: 2025 Skye <57879983+Rainbeon@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Tay <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later AND MIT

using Content.Shared.Examine;
using Content.Shared.Mobs;

namespace Content.Shared.Changeling;

public sealed partial class AbsorbedSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();
        
        // funky station: remove hollowing as is
        //SubscribeLocalEvent<AbsorbedComponent, ExaminedEvent>(OnExamine); 
        SubscribeLocalEvent<AbsorbedComponent, MobStateChangedEvent>(OnMobStateChange);
    }

    private void OnExamine(Entity<AbsorbedComponent> ent, ref ExaminedEvent args)
    {
        args.PushMarkup(Loc.GetString("changeling-absorb-onexamine"));
    }

    private void OnMobStateChange(Entity<AbsorbedComponent> ent, ref MobStateChangedEvent args)
    {
        // in case one somehow manages to dehusk someone
        if (args.NewMobState != MobState.Dead)
            RemComp<AbsorbedComponent>(ent);
    }
}
