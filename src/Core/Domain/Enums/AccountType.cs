using System.ComponentModel;

namespace OpsManagerAPI.Domain.Enums;

public enum AccountType
{
    [Description(nameof(Prepaid))]
    Prepaid = 1,
    [Description(nameof(Postpaid))]
    Postpaid
}
