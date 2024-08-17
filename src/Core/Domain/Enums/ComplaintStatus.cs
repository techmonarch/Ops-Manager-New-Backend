using System.ComponentModel;

namespace OpsManagerAPI.Domain.Enums;

public enum ComplaintStatus
{
    [Description(nameof(Opened))]
    Opened = 1,
    [Description(nameof(Resolved))]
    Resolved,
    [Description(nameof(Closed))]
    Closed,
    [Description(nameof(InfoNeeded))]
    InfoNeeded
}

public static class ComplaintStatusExtension
{
    public static bool CanTransitionTo(this ComplaintStatus current, ComplaintStatus next)
    {
        if (current == ComplaintStatus.Opened && next == ComplaintStatus.InfoNeeded) return true;
        if (current == ComplaintStatus.Opened && next == ComplaintStatus.Resolved) return true;
        if (current == ComplaintStatus.Opened && next == ComplaintStatus.Closed) return true;
        if (current == ComplaintStatus.InfoNeeded && next == ComplaintStatus.Opened) return true;
        if (current == ComplaintStatus.InfoNeeded && next == ComplaintStatus.Resolved) return true;
        if (current == ComplaintStatus.InfoNeeded && next == ComplaintStatus.Closed) return true;
        if (current == ComplaintStatus.Resolved && next == ComplaintStatus.Closed) return true;
        if (current == ComplaintStatus.Closed) return false;
        return false;
    }
}
