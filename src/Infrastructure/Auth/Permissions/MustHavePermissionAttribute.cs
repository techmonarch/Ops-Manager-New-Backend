using Microsoft.AspNetCore.Authorization;
using OpsManagerAPI.Infrastructure.Authorization;

namespace OpsManagerAPI.Infrastructure.Auth.Permissions;
public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string action, string resource) =>
        Policy = OPSPermission.NameFor(action, resource);
}