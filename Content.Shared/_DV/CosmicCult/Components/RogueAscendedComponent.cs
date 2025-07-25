// SPDX-FileCopyrightText: 2025 corresp0nd <46357632+corresp0nd@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 deltanedas <@deltanedas:kde.org>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later AND MIT

using Content.Shared.Damage;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._DV.CosmicCult.Components;

/// <summary>
/// Component to designate a mob as a rogue astral ascendant.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class RogueAscendedComponent : Component
{
    /// <summary>
    /// The duration of our slumber DoAfter.
    /// </summary>
    [DataField]
    public TimeSpan RogueSlumberDoAfterTime = TimeSpan.FromSeconds(1);

    /// <summary>
    /// The duration of our infection DoAfter.
    /// </summary>
    [DataField]
    public TimeSpan RogueInfectionDoAfterTime = TimeSpan.FromSeconds(8);

    /// <summary>
    /// The duration inflicted by Slumber Shell
    /// </summary>
    [DataField]
    public TimeSpan RogueSlumberTime = TimeSpan.FromSeconds(25);

    [DataField]
    public SoundSpecifier InfectionSfx = new SoundPathSpecifier("/Audio/_DV/CosmicCult/ability_nova_impact.ogg");

    [DataField]
    public SoundSpecifier ShatterSfx = new SoundPathSpecifier("/Audio/_DV/CosmicCult/ascendant_shatter.ogg");

    [DataField]
    public SoundSpecifier MobSound = new SoundPathSpecifier("/Audio/_DV/CosmicCult/ascendant_noise.ogg");

    [DataField]
    public EntProtoId Vfx = "CosmicGenericVFX";

    [DataField]
    public TimeSpan StunTime = TimeSpan.FromSeconds(7);

    [DataField]
    public DamageSpecifier InfectionHeal = new()
    {
        DamageDict = new()
        {
            { "Blunt", 25},
            { "Slash", 25},
            { "Piercing", 25},
            { "Heat", 25},
            { "Shock", 25},
            { "Cold", 25},
            { "Poison", 25},
            { "Radiation", 25},
            { "Asphyxiation", 25}
        }
    };

}
