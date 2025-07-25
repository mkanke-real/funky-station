// SPDX-FileCopyrightText: 2025 empty0set <16693552+empty0set@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 empty0set <empty0set@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later AND MIT

using Content.Server.StationEvents.Events;
using Content.Shared.Roles;
using Robust.Shared.Prototypes;

namespace Content.Server.StationEvents.Components;

/// <summary>
/// This is a station event that randomly adds specific jobs for latejoin.
/// </summary>
[RegisterComponent]
[Access(typeof(JobDistributionErrorRule))]
public sealed partial class JobDistributionErrorRuleComponent : Component
{
    /// <summary>
    /// The minimum amount of jobs from list that will be added.
    /// </summary>
    [DataField]
    public int MinJobs = 1;

    /// <summary>
    /// The maximum amount of jobs from list that will be added.
    /// </summary>
    [DataField]
    public int MaxJobs = 2;

    /// <summary>
    /// The minimum amount of slots for selected jobs.
    /// </summary>
    [DataField]
    public int MinAmount = 1;

    /// <summary>
    /// The maximum amount of slots for selected jobs.
    /// </summary>
    [DataField]
    public int MaxAmount = 3;

    /// <summary>
    /// List of jobs that can be added.
    /// </summary>
    [DataField]
    public List<ProtoId<JobPrototype>> Jobs = new();
}
