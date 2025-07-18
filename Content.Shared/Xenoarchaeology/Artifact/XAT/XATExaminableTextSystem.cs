// SPDX-FileCopyrightText: 2025 Tay <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2025 pa.pecherskij <pa.pecherskij@interfax.ru>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.Examine;
using Content.Shared.Xenoarchaeology.Artifact.Components;
using Content.Shared.Xenoarchaeology.Artifact.XAT.Components;

namespace Content.Shared.Xenoarchaeology.Artifact.XAT;

/// <remarks>
/// System for marking xeno artifact with certain text.
/// </remarks>
/// <remarks> Not actually a trigger but nice and easy to use. </remarks>
public sealed class XATExaminableTextSystem : BaseXATSystem<XATExaminableTextComponent>
{
    /// <inheritdoc/>
    public override void Initialize()
    {
        base.Initialize();

        XATSubscribeDirectEvent<ExaminedEvent>(OnExamined);
    }

    private void OnExamined(Entity<XenoArtifactComponent> artifact, Entity<XATExaminableTextComponent, XenoArtifactNodeComponent> node, ref ExaminedEvent args)
    {
        if (!args.IsInDetailsRange)
            return;

        args.PushMarkup(Loc.GetString(node.Comp1.ExamineText));
    }
}
