// SPDX-FileCopyrightText: 2024 SlamBamActionman <83650252+SlamBamActionman@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.EntityEffects;
using Content.Shared.Mobs;
using Content.Shared.Mobs.Components;
using Robust.Shared.Prototypes;

namespace Content.Server.EntityEffects.EffectConditions;

public sealed partial class MobStateCondition : EntityEffectCondition
{
    [DataField]
    public MobState Mobstate = MobState.Alive;

    public override bool Condition(EntityEffectBaseArgs args)
    {
        if (args.EntityManager.TryGetComponent(args.TargetEntity, out MobStateComponent? mobState))
        {
            if (mobState.CurrentState == Mobstate)
                return true;
        }

        return false;
    }

    public override string GuidebookExplanation(IPrototypeManager prototype)
    {
        return Loc.GetString("reagent-effect-condition-guidebook-mob-state-condition", ("state", Mobstate));
    }
}

