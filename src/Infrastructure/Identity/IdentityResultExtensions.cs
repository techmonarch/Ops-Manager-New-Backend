using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace OpsManagerAPI.Infrastructure.Identity;
internal static class IdentityResultExtensions
{
    public static List<string> GetErrors(this IdentityResult result) =>
        result.Errors.Select(e => e.Description.ToString()).ToList();
}