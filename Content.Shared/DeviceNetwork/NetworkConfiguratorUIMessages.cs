// SPDX-FileCopyrightText: 2023 AJCM-git <60196617+AJCM-git@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Julian Giebel <juliangiebel@live.de>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.Serialization;

namespace Content.Shared.DeviceNetwork;

[Serializable, NetSerializable]
public enum NetworkConfiguratorUiKey
{
    List,
    Configure,
    Link
}

[Serializable, NetSerializable]
public enum NetworkConfiguratorButtonKey
{
    Set,
    Add,
    Edit,
    Clear,
    Copy,
    Show
}

/// <summary>
/// Message sent when the remove button for one device on the list was pressed
/// </summary>
[Serializable, NetSerializable]
public sealed class NetworkConfiguratorRemoveDeviceMessage : BoundUserInterfaceMessage
{
    public readonly string Address;

    public NetworkConfiguratorRemoveDeviceMessage(string address)
    {
        Address = address;
    }
}

/// <summary>
/// Message sent when the clear button was pressed
/// </summary>
[Serializable, NetSerializable]
public sealed class NetworkConfiguratorClearDevicesMessage : BoundUserInterfaceMessage
{
}

[Serializable, NetSerializable]
public sealed class NetworkConfiguratorButtonPressedMessage : BoundUserInterfaceMessage
{
    public readonly NetworkConfiguratorButtonKey ButtonKey;

    public NetworkConfiguratorButtonPressedMessage(NetworkConfiguratorButtonKey buttonKey)
    {
        ButtonKey = buttonKey;
    }
}

[Serializable, NetSerializable]
public sealed class NetworkConfiguratorClearLinksMessage : BoundUserInterfaceMessage
{

}

[Serializable, NetSerializable]
public sealed class NetworkConfiguratorToggleLinkMessage : BoundUserInterfaceMessage
{
    public readonly string Source;
    public readonly string Sink;

    public NetworkConfiguratorToggleLinkMessage(string source, string sink)
    {
        Source = source;
        Sink = sink;
    }
}

[Serializable, NetSerializable]
public sealed class NetworkConfiguratorLinksSaveMessage : BoundUserInterfaceMessage
{
    public readonly List<(string source, string sink)> Links;

    public NetworkConfiguratorLinksSaveMessage(List<(string source, string sink)> links)
    {
        Links = links;
    }
}
