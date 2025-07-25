// SPDX-FileCopyrightText: 2025 misghast <51974455+misterghast@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later AND MIT

using Content.Server.StationEvents.Events;
using Content.Shared.FixedPoint;

namespace Content.Server._Goobstation.StationEvents.Metric.Components;

[RegisterComponent, Access(typeof(DoorMetricSystem))]
public sealed partial class DoorMetricComponent : Component
{
    /// <summary>
    ///   Cost of all doors emagged door
    /// </summary>
    [DataField("emagCost"), ViewVariables(VVAccess.ReadWrite)]
    public float EmagCost = 200.0f;

    /// <summary>
    ///   Cost of all doors with no power
    /// </summary>
    [DataField("powerCost"), ViewVariables(VVAccess.ReadWrite)]
    public float PowerCost = 100.0f;

    /// <summary>
    ///   Cost of all firedoors holding pressure
    /// </summary>
    [DataField("pressureCost"), ViewVariables(VVAccess.ReadWrite)]
    public float PressureCost = 200.0f;

    /// <summary>
    ///   Cost of all firedoors holding temperature
    /// </summary>
    [DataField("fireCost"), ViewVariables(VVAccess.ReadWrite)]
    public float FireCost = 400.0f;
}
