// SPDX-FileCopyrightText: 2025 corresp0nd <46357632+corresp0nd@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 deltanedas <@deltanedas:kde.org>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later AND MIT

using Content.Shared.Damage;
using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Server._DV.CosmicCult.Components;

[RegisterComponent, AutoGenerateComponentState]
[Access(typeof(DeconversionSystem))]
public sealed partial class CleanseOnUseComponent : Component
{
    [DataField] public TimeSpan UseTime = TimeSpan.FromSeconds(10);

    [DataField] public SoundSpecifier SizzleSound = new SoundPathSpecifier("/Audio/Effects/lightburn.ogg");

    [DataField] public SoundSpecifier CleanseSound = new SoundPathSpecifier("/Audio/_DV/CosmicCult/cleanse_deconversion.ogg");

    [DataField] public SoundSpecifier MalignSound = new SoundPathSpecifier("/Audio/_DV/CosmicCult/glyph_trigger.ogg");

    [DataField] public EntProtoId CleanseVFX = "NoosphericVFX2";
    [DataField] public EntProtoId ReboundVFX = "NoosphericVFX1";
    [DataField] public EntProtoId MalignVFX = "CosmicGenericVFX";

    [DataField] public bool Enabled = true;

    /// <summary>
    /// When True allows an item to purge the Cosmic Cult's Malign Rifts onInteractInHand, utilized exclusively by the CosmicRiftSystem.
    /// </summary>
    [DataField] public bool CanPurge;

    [DataField]
    public DamageSpecifier SelfDamage = new()
    {
        DamageDict = new() {
            { "Caustic", 15 }
        }
    };

}
