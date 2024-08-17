using System.ComponentModel;

namespace OpsManagerAPI.Domain.Enums;
public enum Gender
{
    [Description(nameof(Male))]
    Male = 1,
    [Description(nameof(Female))]
    Female
}
