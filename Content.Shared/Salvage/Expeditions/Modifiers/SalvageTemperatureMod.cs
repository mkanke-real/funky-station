// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 deltanedas <39013340+deltanedas@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 deltanedas <@deltanedas:kde.org>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 chavonadelal <156101927+chavonadelal@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Tay <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype.List;

namespace Content.Shared.Salvage.Expeditions.Modifiers;

[Prototype("salvageTemperatureMod")]
public sealed partial class SalvageTemperatureMod : IPrototype, IBiomeSpecificMod
{
    [IdDataField] public string ID { get; private set; } = default!;

    [DataField("desc")] public LocId Description { get; private set; } = string.Empty;

    /// <inheritdoc/>
    [DataField("cost")]
    public float Cost { get; private set; } = 0f;

    /// <inheritdoc/>
    [DataField("biomes", customTypeSerializer: typeof(PrototypeIdListSerializer<SalvageBiomeModPrototype>))]
    public List<string>? Biomes { get; private set; } = null;

    /// <summary>
    /// Temperature in the planets air mix.
    /// </summary>
    [DataField("temperature")]
    public float Temperature = 293.15f;
}
