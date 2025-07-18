// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Ygg01 <y.laughing.man.y@gmail.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Tay <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2025 pa.pecherskij <pa.pecherskij@interfax.ru>
// SPDX-FileCopyrightText: 2025 slarticodefast <161409025+slarticodefast@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.Maps;
using Content.Shared.Tag;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared.Procedural;

[Prototype]
public sealed partial class DungeonRoomPrototype : IPrototype
{
    [IdDataField] public string ID { get; private set; } = string.Empty;

    [ViewVariables(VVAccess.ReadWrite), DataField]
    public List<ProtoId<TagPrototype>> Tags = new();

    [DataField(required: true)]
    public Vector2i Size;

    /// <summary>
    /// Path to the file to use for the room.
    /// </summary>
    [DataField("atlas", required: true)]
    public ResPath AtlasPath;

    /// <summary>
    /// Tile offset into the atlas to use for the room.
    /// </summary>
    [DataField(required: true)]
    public Vector2i Offset;

    /// <summary>
    /// These tiles will be ignored when copying from the atlas into the actual game,
    /// allowing you to make rooms of irregular shapes that blend seamlessly into their surroundings
    /// </summary>
    [DataField]
    public ProtoId<ContentTileDefinition>? IgnoreTile;
}
