// SPDX-FileCopyrightText: 2022 Júlio César Ueti <52474532+Mirino97@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Shared.Movement.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class JetpackComponent : Component
{
    [ViewVariables(VVAccess.ReadWrite), DataField("moleUsage")]
    public float MoleUsage = 0.012f;

    [DataField] public EntProtoId ToggleAction = "ActionToggleJetpack";

    [DataField, AutoNetworkedField] public EntityUid? ToggleActionEntity;

    [ViewVariables(VVAccess.ReadWrite), DataField("acceleration")]
    public float Acceleration = 1f;

    [ViewVariables(VVAccess.ReadWrite), DataField("friction")]
    public float Friction = 0.3f;

    [ViewVariables(VVAccess.ReadWrite), DataField("weightlessModifier")]
    public float WeightlessModifier = 1.2f;
}
