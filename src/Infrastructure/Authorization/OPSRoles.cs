using System.Collections.ObjectModel;

namespace OpsManagerAPI.Infrastructure.Authorization;
public static class OPSRoles
{
    public const string Admin = nameof(Admin);
    public const string BHTE = nameof(BHTE);
    public const string Basic = nameof(Basic);
    public const string CLW = nameof(CLW);

    public static IReadOnlyList<string> DefaultRoles { get; } = new ReadOnlyCollection<string>(new[]
    {
        Admin,
        Basic,
        CLW,
        BHTE,
    });

    public static bool IsDefault(string roleName) => DefaultRoles.Any(r => r == roleName);
}