using Microsoft.AspNetCore.Authorization;
using OpsManagerAPI.Application.Identity.Users;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.Infrastructure.Exceptions;

namespace OpsManagerAPI.Infrastructure.Auth.Permissions;

internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IUserService _userService;

    public PermissionAuthorizationHandler(IUserService userService) =>
        _userService = userService;

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.User?.GetUserId() is { } userId)
        {
            if (await _userService.HasPermissionAsync(userId, requirement.Permission))
            {
                context.Succeed(requirement);
            }
            else
            {
                throw new PermissionDeniedException(requirement.Permission);
            }
        }
    }
}
