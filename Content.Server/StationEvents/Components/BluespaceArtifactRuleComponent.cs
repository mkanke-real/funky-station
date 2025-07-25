// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Server.StationEvents.Events;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Server.StationEvents.Components;

/// <summary>
/// This is used for an event that spawns an artifact
/// somewhere random on the station.
/// </summary>
[RegisterComponent, Access(typeof(BluespaceArtifactRule))]
public sealed partial class BluespaceArtifactRuleComponent : Component
{
    [DataField("artifactSpawnerPrototype", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string ArtifactSpawnerPrototype = "RandomArtifactSpawner";

    [DataField("artifactFlashPrototype", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string ArtifactFlashPrototype = "EffectFlashBluespace";

    [DataField("possibleSightings")]
    public List<string> PossibleSighting = new()
    {
        "bluespace-artifact-sighting-1",
        "bluespace-artifact-sighting-2",
        "bluespace-artifact-sighting-3",
        "bluespace-artifact-sighting-4",
        "bluespace-artifact-sighting-5",
        "bluespace-artifact-sighting-6",
        "bluespace-artifact-sighting-7"
    };
}
