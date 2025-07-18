// SPDX-FileCopyrightText: 2024 Ed <96445749+TheShuEd@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.Player;

namespace Content.Shared.UserInterface;
public sealed class ActivatableUIOpenAttemptEvent : CancellableEntityEventArgs
{
    public EntityUid User { get; }
    public ActivatableUIOpenAttemptEvent(EntityUid who)
    {
        User = who;
    }
}

public sealed class UserOpenActivatableUIAttemptEvent : CancellableEntityEventArgs //have to one-up the already stroke-inducing name
{
    public EntityUid User { get; }
    public EntityUid Target { get; }
    public UserOpenActivatableUIAttemptEvent(EntityUid who, EntityUid target)
    {
        User = who;
        Target = target;
    }
}

public sealed class AfterActivatableUIOpenEvent : EntityEventArgs
{
    public EntityUid User { get; }
    public readonly EntityUid Actor;

    public AfterActivatableUIOpenEvent(EntityUid who, EntityUid actor)
    {
        User = who;
        Actor = actor;
    }
}

/// <summary>
/// This is after it's decided the user can open the UI,
/// but before the UI actually opens.
/// Use this if you need to prepare the UI itself
/// </summary>
public sealed class BeforeActivatableUIOpenEvent : EntityEventArgs
{
    public EntityUid User { get; }
    public BeforeActivatableUIOpenEvent(EntityUid who)
    {
        User = who;
    }
}

public sealed class ActivatableUIPlayerChangedEvent : EntityEventArgs
{
}
