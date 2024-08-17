using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Exceptions;
using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Identity.Users;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.Infrastructure.Multitenancy;

namespace OpsManagerAPI.Infrastructure.Identity;
internal partial class UserService
{
    public async Task<ApiResponse<List<UserRoleDto>>> GetRolesAsync(string userId, CancellationToken cancellationToken)
    {
        var userRoles = new List<UserRoleDto>();

        var user = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User Not Found.");
        var roles = await _roleManager.Roles.AsNoTracking().ToListAsync(cancellationToken) ?? throw new NotFoundException("Roles Not Found.");
        foreach (var role in roles)
        {
            userRoles.Add(new UserRoleDto
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Description = role.Description,
                Enabled = await _userManager.IsInRoleAsync(user, role.Name!)
            });
        }

        return new ApiResponse<List<UserRoleDto>>(true, "Roles retrieevd successfully", userRoles);
    }

    public async Task<ApiResponse<string>> AssignRolesAsync(string userId, UserRolesRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException("User Not Found.");

        // Check if the user is an admin for which the admin role is getting disabled
        if (await _userManager.IsInRoleAsync(user, OPSRoles.Admin)
            && request.UserRoles.Any(a => !a.Enabled && a.RoleName == OPSRoles.Admin))
        {
            // Get count of users in Admin Role
            int adminCount = (await _userManager.GetUsersInRoleAsync(OPSRoles.Admin)).Count;

            // Check if user is not Root Tenant Admin
            // Edge Case : there are chances for other tenants to have users with the same email as that of Root Tenant Admin. Probably can add a check while User Registration
            if (user.Email == MultitenancyConstants.Root.EmailAddress)
            {
                if (_currentTenant.Id == MultitenancyConstants.Root.Id)
                {
                    throw new ConflictException("Cannot Remove Admin Role From Root Tenant Admin.");
                }
            }
            else if (adminCount <= 2)
            {
                throw new ConflictException("Tenant should have at least 2 Admins.");
            }
        }

        foreach (var userRole in request.UserRoles)
        {
            // Check if Role Exists
            if (await _roleManager.FindByNameAsync(userRole.RoleName!) is not null)
            {
                if (userRole.Enabled)
                {
                    if (!await _userManager.IsInRoleAsync(user, userRole.RoleName!))
                    {
                        await _userManager.AddToRoleAsync(user, userRole.RoleName!);
                    }
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, userRole.RoleName!);
                }
            }
        }

        return new ApiResponse<string>(true, "User Roles Updated Successfully.", "User Roles Updated Successfully.");
    }
}