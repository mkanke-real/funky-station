// SPDX-FileCopyrightText: 2022 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Brandon Hu <103440971+Brandon-Huu@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Tay <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2025 pa.pecherskij <pa.pecherskij@interfax.ru>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using System.Linq;
using Content.Server.Administration.Notes;
using Content.Shared.Administration;
using Robust.Server.Player;
using Robust.Shared.Console;

namespace Content.Server.Administration.Commands;

[AdminCommand(AdminFlags.ViewNotes)]
public sealed class OpenAdminNotesCommand : LocalizedCommands
{
    public const string CommandName = "adminnotes";

    public override string Command => CommandName;

    public override async void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        if (shell.Player is not { } player)
        {
            shell.WriteError(Loc.GetString("shell-cannot-run-command-from-server"));
            return;
        }

        Guid notedPlayer;

        switch (args.Length)
        {
            case 1 when Guid.TryParse(args[0], out notedPlayer):
                break;
            case 1:
                var locator = IoCManager.Resolve<IPlayerLocator>();
                var dbGuid = await locator.LookupIdByNameAsync(args[0]);

                if (dbGuid == null)
                {
                    shell.WriteError(Loc.GetString("cmd-adminnotes-wrong-target", ("user", args[0])));
                    return;
                }

                notedPlayer = dbGuid.UserId;
                break;
            default:
                shell.WriteError(Loc.GetString("cmd-adminnotes-args-error"));
                return;
        }

        await IoCManager.Resolve<IAdminNotesManager>().OpenEui(player, notedPlayer);
    }

    public override CompletionResult GetCompletion(IConsoleShell shell, string[] args)
    {
        if (args.Length != 1)
            return CompletionResult.Empty;

        var playerMgr = IoCManager.Resolve<IPlayerManager>();
        var options = playerMgr.Sessions.Select(c => c.Name).OrderBy(c => c).ToArray();
        return CompletionResult.FromHintOptions(options, Loc.GetString("cmd-adminnotes-hint"));
    }
}
