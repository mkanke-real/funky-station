// SPDX-FileCopyrightText: 2022 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 deltanedas <39013340+deltanedas@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 deltanedas <@deltanedas:kde.org>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Client.UserInterface.Fragments;
using Content.Shared.Mech;
using Robust.Client.GameObjects;
using Robust.Client.UserInterface;

namespace Content.Client.Mech.Ui.Equipment;

public sealed partial class MechSoundboardUi : UIFragment
{
    private MechSoundboardUiFragment? _fragment;

    public override Control GetUIFragmentRoot()
    {
        return _fragment!;
    }

    public override void Setup(BoundUserInterface userInterface, EntityUid? fragmentOwner)
    {
        if (fragmentOwner == null)
            return;

        _fragment = new MechSoundboardUiFragment();
        _fragment.OnPlayAction += sound =>
        {
            // TODO: IDK dog
            userInterface.SendMessage(new MechSoundboardPlayMessage(IoCManager.Resolve<IEntityManager>().GetNetEntity(fragmentOwner.Value), sound));
        };
    }

    public override void UpdateState(BoundUserInterfaceState state)
    {
        if (state is not MechSoundboardUiState soundboardState)
            return;

        _fragment?.UpdateContents(soundboardState);
    }
}
