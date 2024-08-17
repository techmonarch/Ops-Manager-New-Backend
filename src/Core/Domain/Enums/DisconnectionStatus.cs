using System.ComponentModel;

namespace OpsManagerAPI.Domain.Enums;

public enum DisconnectionStatus
{
    [Description(nameof(Pending))]
    Pending = 1,
    [Description(nameof(Approved))]
    Approved,
    [Description(nameof(Disapproved))]
    Disapproved,
    [Description(nameof(Assigned))]
    Assigned,
    [Description(nameof(Disconnected))]
    Disconnected,
    [Description(nameof(NotDisconnected))]
    NotDisconnected,
}

public static class DisconnectionStatusExtension
{
    public static bool CanTransitionTo(this DisconnectionStatus current, DisconnectionStatus next)
    {
        if (current == DisconnectionStatus.Pending && next == DisconnectionStatus.Approved) return true;
        if (current == DisconnectionStatus.Pending && next == DisconnectionStatus.Disapproved) return true;
        if (current == DisconnectionStatus.Approved && next == DisconnectionStatus.Assigned) return true;
        if (current == DisconnectionStatus.Assigned && next == DisconnectionStatus.Disconnected) return true;
        if (current == DisconnectionStatus.Assigned && next == DisconnectionStatus.NotDisconnected) return true;
        if (current == DisconnectionStatus.Disconnected) return false;
        if (current == DisconnectionStatus.NotDisconnected) return false;
        return false;
    }
}