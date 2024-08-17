using System.ComponentModel;

namespace OpsManagerAPI.Domain.Enums;

public enum ReconnectionStatus
{
    [Description(nameof(Pending))]
    Pending = 1,
    [Description(nameof(Approved))]
    Approved,
    [Description(nameof(Disapproved))]
    Disapproved,
    [Description(nameof(Assigned))]
    Assigned,
    [Description(nameof(Reconnected))]
    Reconnected,
    [Description(nameof(NotReconnected))]
    NotReconnected
}

public static class ReconnectionStatusExtension
{
    public static bool CanTransitionTo(this ReconnectionStatus current, ReconnectionStatus next)
    {
        if (current == ReconnectionStatus.Pending && next == ReconnectionStatus.Approved) return true;
        if (current == ReconnectionStatus.Pending && next == ReconnectionStatus.Disapproved) return true;
        if (current == ReconnectionStatus.Approved && next == ReconnectionStatus.Assigned) return true;
        if (current == ReconnectionStatus.Assigned && next == ReconnectionStatus.Reconnected) return true;
        if (current == ReconnectionStatus.Assigned && next == ReconnectionStatus.NotReconnected) return true;
        if (current == ReconnectionStatus.Reconnected) return false;
        if (current == ReconnectionStatus.NotReconnected) return false;
        return false;
    }
}