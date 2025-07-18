// SPDX-FileCopyrightText: 2024 Aidenkrz <aiden@djkraz.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later AND MIT

using Robust.Shared.GameStates;
using Robust.Shared.Serialization;

namespace Content.Shared.Carrying
{
    [RegisterComponent, NetworkedComponent, Access(typeof(CarryingSlowdownSystem))]

    public sealed partial class CarryingSlowdownComponent : Component
    {
        [DataField("walkModifier", required: true)] [ViewVariables(VVAccess.ReadWrite)]
        public float WalkModifier = 1.0f;

        [DataField("sprintModifier", required: true)] [ViewVariables(VVAccess.ReadWrite)]
        public float SprintModifier = 1.0f;
    }

    [Serializable, NetSerializable]
    public sealed class CarryingSlowdownComponentState : ComponentState
    {
        public float WalkModifier;
        public float SprintModifier;
        public CarryingSlowdownComponentState(float walkModifier, float sprintModifier)
        {
            WalkModifier = walkModifier;
            SprintModifier = sprintModifier;
        }
    }
}
