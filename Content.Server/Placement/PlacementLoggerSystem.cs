// SPDX-FileCopyrightText: 2023 Chief-Engineer <119664036+Chief-Engineer@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Tay <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2025 pa.pecherskij <pa.pecherskij@interfax.ru>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Server.Administration.Logs;
using Content.Shared.Database;
using Robust.Shared.Map;
using Robust.Shared.Placement;
using Robust.Shared.Player;

namespace Content.Server.Placement;

public sealed class PlacementLoggerSystem : EntitySystem
{
    [Dependency] private readonly IAdminLogManager _adminLogger = default!;
    [Dependency] private readonly ITileDefinitionManager _tileDefinitionManager = default!;
    [Dependency] private readonly ISharedPlayerManager _player = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<PlacementEntityEvent>(OnEntityPlacement);
        SubscribeLocalEvent<PlacementTileEvent>(OnTilePlacement);
    }

    private void OnEntityPlacement(PlacementEntityEvent ev)
    {
        _player.TryGetSessionById(ev.PlacerNetUserId, out var actor);
        var actorEntity = actor?.AttachedEntity;

        var logType = ev.PlacementEventAction switch
        {
            PlacementEventAction.Create => LogType.EntitySpawn,
            PlacementEventAction.Erase => LogType.EntityDelete,
            _ => LogType.Action
        };

        if (actorEntity != null)
            _adminLogger.Add(logType, LogImpact.Medium,
                $"{ToPrettyString(actorEntity.Value):actor} used placement system to {ev.PlacementEventAction.ToString().ToLower()} {ToPrettyString(ev.EditedEntity):subject} at {ev.Coordinates}");
        else if (actor != null)
            _adminLogger.Add(logType, LogImpact.Medium,
                $"{actor:actor} used placement system to {ev.PlacementEventAction.ToString().ToLower()} {ToPrettyString(ev.EditedEntity):subject} at {ev.Coordinates}");
        else
            _adminLogger.Add(logType, LogImpact.Medium,
                $"Placement system {ev.PlacementEventAction.ToString().ToLower()}ed {ToPrettyString(ev.EditedEntity):subject} at {ev.Coordinates}");
    }

    private void OnTilePlacement(PlacementTileEvent ev)
    {
        _player.TryGetSessionById(ev.PlacerNetUserId, out var actor);
        var actorEntity = actor?.AttachedEntity;

        if (actorEntity != null)
            _adminLogger.Add(LogType.Tile, LogImpact.Medium,
                $"{ToPrettyString(actorEntity.Value):actor} used placement system to set tile {_tileDefinitionManager[ev.TileType].Name} at {ev.Coordinates}");
        else if (actor != null)
            _adminLogger.Add(LogType.Tile, LogImpact.Medium,
                $"{actor} used placement system to set tile {_tileDefinitionManager[ev.TileType].Name} at {ev.Coordinates}");
        else
            _adminLogger.Add(LogType.Tile, LogImpact.Medium,
                $"Placement system set tile {_tileDefinitionManager[ev.TileType].Name} at {ev.Coordinates}");
    }
}
