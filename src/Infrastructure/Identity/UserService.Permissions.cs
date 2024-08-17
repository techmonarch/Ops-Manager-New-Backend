using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Exceptions;
using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Infrastructure.Authorization;

namespace OpsManagerAPI.Infrastructure.Identity;
internal partial class UserService
{
    public async Task<ApiResponse<List<string>>> GetPermissionsAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new UnauthorizedException("Authentication Failed.");

        var userRoles = await _userManager.GetRolesAsync(user);
        var permissions = new List<string>();
        foreach (var role in await _roleManager.Roles
            .Where(r => userRoles.Contains(r.Name!))
            .ToListAsync(cancellationToken))
        {
            permissions.AddRange(await _db.RoleClaims
                .Where(rc => rc.RoleId == role.Id && rc.ClaimType == OPSClaims.Permission)
                .Select(rc => rc.ClaimValue!)
                .ToListAsync(cancellationToken));
        }

        return new ApiResponse<List<string>>(true, "Permission retrieevd successfully", permissions.Distinct().ToList());
    }

    public async Task<bool> HasPermissionAsync(string userId, string permission, CancellationToken cancellationToken)
    {
        var permissions = await GetPermissionsAsync(userId, cancellationToken);

        return permissions?.Data?.Contains(permission) ?? false;
    }
}