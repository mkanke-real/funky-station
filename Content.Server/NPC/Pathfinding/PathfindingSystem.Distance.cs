// SPDX-FileCopyrightText: 2023 Visne <39844191+Visne@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Aidenkrz <aiden@djkraz.com>
// SPDX-FileCopyrightText: 2024 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 TemporalOroboros <TemporalOroboros@gmail.com>
// SPDX-FileCopyrightText: 2024 eoineoineoin <github@eoinrul.es>
// SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Tay <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2025 slarticodefast <161409025+slarticodefast@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using System.Numerics;
using Content.Shared.NPC;

namespace Content.Server.NPC.Pathfinding;

public sealed partial class PathfindingSystem
{
    public float EuclideanDistance(PathPoly start, PathPoly end)
    {
        var (dx, dy) = GetDiff(start, end);
        return MathF.Sqrt((dx * dx + dy * dy));
    }

    public float ManhattanDistance(PathPoly start, PathPoly end)
    {
        var (dx, dy) = GetDiff(start, end);
        return dx + dy;
    }

    public float OctileDistance(PathPoly start, PathPoly end)
    {
        var (dx, dy) = GetDiff(start, end);
        return dx + dy + (1.41f - 2) * Math.Min(dx, dy);
    }

    private Vector2 GetDiff(PathPoly start, PathPoly end)
    {
        var startPos = start.Box.Center;
        var endPos = end.Box.Center;

        if (end.GraphUid != start.GraphUid)
        {
            if (!TryComp(start.GraphUid, out TransformComponent? startXform) ||
                !TryComp(end.GraphUid, out TransformComponent? endXform))
            {
                return Vector2.Zero;
            }

            endPos = Vector2.Transform(Vector2.Transform(endPos, _transform.GetWorldMatrix(endXform)), _transform.GetInvWorldMatrix(startXform));
        }

        // TODO: Numerics when we changeover.
        var diff = startPos - endPos;
        var ab = Vector2.Abs(diff);
        return ab;
    }
}
