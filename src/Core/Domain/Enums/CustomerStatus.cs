using System.ComponentModel;

namespace OpsManagerAPI.Domain.Enums;

public enum CustomerStatus
{
    [Description(nameof(Active))]
    Active = 1,

    [Description(nameof(Inactive))]
    Inactive,

    [Description(nameof(Suspended))]
    Suspended,

    [Description(nameof(Duplicate))]
    Duplicate,

    [Description(nameof(DoesNotExist))]
    DoesNotExist
}