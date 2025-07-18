// SPDX-FileCopyrightText: 2023 chromiumboy <50505512+chromiumboy@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Tay <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2025 slarticodefast <161409025+slarticodefast@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.Power;
using Robust.Client.UserInterface;

namespace Content.Client.Power;

public sealed class PowerMonitoringConsoleBoundUserInterface : BoundUserInterface
{
    [ViewVariables]
    private PowerMonitoringWindow? _menu;

    public PowerMonitoringConsoleBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey) { }

    protected override void Open()
    {
        base.Open();

        _menu = this.CreateWindow<PowerMonitoringWindow>();
        _menu.SetEntity(Owner);
        _menu.SendPowerMonitoringConsoleMessageAction += SendPowerMonitoringConsoleMessage;
    }

    protected override void UpdateState(BoundUserInterfaceState state)
    {
        base.UpdateState(state);

        var castState = (PowerMonitoringConsoleBoundInterfaceState) state;

        EntMan.TryGetComponent<TransformComponent>(Owner, out var xform);
        _menu?.ShowEntites
            (castState.TotalSources,
            castState.TotalBatteryUsage,
            castState.TotalLoads,
            castState.AllEntries,
            castState.FocusSources,
            castState.FocusLoads,
            xform?.Coordinates);
    }

    public void SendPowerMonitoringConsoleMessage(NetEntity? netEntity, PowerMonitoringConsoleGroup group)
    {
        SendMessage(new PowerMonitoringConsoleMessage(netEntity, group));
    }
}
