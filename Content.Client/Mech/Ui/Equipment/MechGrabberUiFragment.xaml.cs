// SPDX-FileCopyrightText: 2022 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.Mech;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;

namespace Content.Client.Mech.Ui.Equipment;

[GenerateTypedNameReferences]
public sealed partial class MechGrabberUiFragment : BoxContainer
{
    [Dependency] private readonly IEntityManager _entity = default!;

    public event Action<EntityUid>? OnEjectAction;

    public MechGrabberUiFragment()
    {
        RobustXamlLoader.Load(this);
        IoCManager.InjectDependencies(this);
    }

    public void UpdateContents(MechGrabberUiState state)
    {
        SpaceLabel.Text = $"{state.Contents.Count}/{state.MaxContents}";
        for (var i = 0; i < state.Contents.Count; i++)
        {
            var ent = _entity.GetEntity(state.Contents[i]);

            if (!_entity.TryGetComponent<MetaDataComponent>(ent, out var meta))
                continue;

            ItemList.AddItem(meta.EntityName);
            ItemList[i].OnSelected += _ => OnEjectAction?.Invoke(ent);
        }
    }
}
