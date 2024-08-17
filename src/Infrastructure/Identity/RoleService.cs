using Finbuckle.MultiTenant;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Exceptions;
using OpsManagerAPI.Application.Common.Interfaces;
using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Identity.Roles;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.Infrastructure.Multitenancy;
using OpsManagerAPI.Infrastructure.Persistence.Context;

namespace OpsManagerAPI.Infrastructure.Identity;
internal class RoleService : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _db;
    private readonly ICurrentUser _currentUser;
    private readonly ITenantInfo _currentTenant;

    public RoleService(
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext db,
        ICurrentUser currentUser,
        ITenantInfo currentTenant)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _db = db;
        _currentUser = currentUser;
        _currentTenant = currentTenant;
    }

    public async Task<ApiResponse<List<RoleDto>>> GetListAsync(CancellationToken cancellationToken) =>
        new ApiResponse<List<RoleDto>>(true, "Roles retrieved Successfully", (await _roleManager.Roles.ToListAsync(cancellationToken))
            .Adapt<List<RoleDto>>());

    public async Task<ApiResponse<int>> GetCountAsync(CancellationToken cancellationToken) =>
        new ApiResponse<int>(true, "Successful", await _roleManager.Roles.CountAsync(cancellationToken));

    public async Task<bool> ExistsAsync(string roleName, string? excludeId) =>
        await _roleManager.FindByNameAsync(roleName)
            is ApplicationRole existingRole
            && existingRole.Id != excludeId;

    public async Task<ApiResponse<RoleDto>> GetByIdAsync(string id) =>
        new ApiResponse<RoleDto>(true, "Roles retrieved Successfully", await _db.Roles.SingleOrDefaultAsync(x => x.Id == id) is { } role
            ? role.Adapt<RoleDto>()
            : throw new NotFoundException("Role Not Found"));

    public async Task<ApiResponse<RoleDto>> GetByIdWithPermissionsAsync(string roleId, CancellationToken cancellationToken)
    {
        var role = await GetByIdAsync(roleId);

        role.Data.Permissions = await _db.RoleClaims
            .Where(c => c.RoleId == roleId && c.ClaimType == OPSClaims.Permission)
            .Select(c => c.ClaimValue!)
            .ToListAsync(cancellationToken);

        return new ApiResponse<RoleDto>(true, "Role retrieved Successfully", role.Data);
    }

    public async Task<ApiResponse<string>> CreateOrUpdateAsync(CreateOrUpdateRoleRequest request)
    {
        if (string.IsNullOrEmpty(request.Id))
        {
            // Create a new role.
            var role = new ApplicationRole(request.Name, request.Description);
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                throw new InternalServerException("Register role failed", result.GetErrors());
            }

            return new ApiResponse<string>(true, string.Format("Role {0} Created.", request.Name), string.Format("Role {0} Created.", request.Name));
        }
        else
        {
            // Update an existing role.
            var role = await _roleManager.FindByIdAsync(request.Id);

            _ = role ?? throw new NotFoundException("Role Not Found");

            if (OPSRoles.IsDefault(role.Name!))
            {
                throw new ConflictException(string.Format("Not allowed to modify {0} Role.", role.Name));
            }

            role.Name = request.Name;
            role.NormalizedName = request.Name.ToUpperInvariant();
            role.Description = request.Description;
            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                throw new InternalServerException("Update role failed", result.GetErrors());
            }

            return new ApiResponse<string>(true, string.Format("Role {0} Updated.", role.Name), string.Format("Role {0} Updated.", role.Name));
        }
    }

    public async Task<ApiResponse<string>> UpdatePermissionsAsync(UpdateRolePermissionsRequest request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId);
        _ = role ?? throw new NotFoundException("Role Not Found");
        if (role.Name == OPSRoles.Admin)
        {
            throw new ConflictException("Not allowed to modify Permissions for this Role.");
        }

        if (_currentTenant.Id != MultitenancyConstants.Root.Id)
        {
            // Remove Root Permissions if the Role is not created for Root Tenant.
            request.Permissions.RemoveAll(u => u.StartsWith("Permissions.Root."));
        }

        var currentClaims = await _roleManager.GetClaimsAsync(role);

        // Remove permissions that were previously selected
        foreach (var claim in currentClaims.Where(c => !request.Permissions.Any(p => p == c.Value)))
        {
            var removeResult = await _roleManager.RemoveClaimAsync(role, claim);
            if (!removeResult.Succeeded)
            {
                throw new InternalServerException("Update permissions failed.", removeResult.GetErrors());
            }
        }

        // Add all permissions that were not previously selected
        foreach (string permission in request.Permissions.Where(c => !currentClaims.Any(p => p.Value == c)))
        {
            if (!string.IsNullOrEmpty(permission))
            {
                _db.RoleClaims.Add(new ApplicationRoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = OPSClaims.Permission,
                    ClaimValue = permission,
                    CreatedBy = _currentUser.GetUserId().ToString()
                });
                await _db.SaveChangesAsync(cancellationToken);
            }
        }

        return new ApiResponse<string>(true, "Permissions Updated.", "Permissions Updated.");
    }

    public async Task<ApiResponse<string>> DeleteAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);

        _ = role ?? throw new NotFoundException("Role Not Found");

        if (OPSRoles.IsDefault(role.Name!))
        {
            throw new ConflictException(string.Format("Not allowed to delete {0} Role.", role.Name));
        }

        if ((await _userManager.GetUsersInRoleAsync(role.Name!)).Count > 0)
        {
            throw new ConflictException(string.Format("Not allowed to delete {0} Role as it is being used.", role.Name));
        }

        await _roleManager.DeleteAsync(role);

        return new ApiResponse<string>(true, string.Format("Role {0} Deleted.", role.Name), string.Format("Role {0} Deleted.", role.Name));
    }
}