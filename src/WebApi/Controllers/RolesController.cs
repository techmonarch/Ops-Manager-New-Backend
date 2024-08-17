using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Identity.Roles;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;
public class RolesController : VersionNeutralApiController
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService) => _roleService = roleService;

    [HttpGet]
    [MustHavePermission(OPSAction.View, OPSResource.Roles)]
    [OpenApiOperation("Get a list of all roles.", "")]
    public Task<ApiResponse<List<RoleDto>>> GetListAsync(CancellationToken cancellationToken)
    {
        return _roleService.GetListAsync(cancellationToken);
    }

    [HttpGet("{id}")]
    [MustHavePermission(OPSAction.View, OPSResource.Roles)]
    [OpenApiOperation("Get role details.", "")]
    public Task<ApiResponse<RoleDto>> GetByIdAsync(string id)
    {
        return _roleService.GetByIdAsync(id);
    }

    [HttpGet("{id}/permissions")]
    [MustHavePermission(OPSAction.View, OPSResource.RoleClaims)]
    [OpenApiOperation("Get role details with its permissions.", "")]
    public Task<ApiResponse<RoleDto>> GetByIdWithPermissionsAsync(string id, CancellationToken cancellationToken)
    {
        return _roleService.GetByIdWithPermissionsAsync(id, cancellationToken);
    }

    [HttpPut("{id}/permissions")]
    [MustHavePermission(OPSAction.Update, OPSResource.RoleClaims)]
    [OpenApiOperation("Update a role's permissions.", "")]
    public async Task<ActionResult<ApiResponse<string>>> UpdatePermissionsAsync(string id, UpdateRolePermissionsRequest request, CancellationToken cancellationToken)
    {
        if (id != request.RoleId)
        {
            return BadRequest();
        }

        return Ok(await _roleService.UpdatePermissionsAsync(request, cancellationToken));
    }

    [HttpPost]
    [MustHavePermission(OPSAction.Create, OPSResource.Roles)]
    [OpenApiOperation("Create or update a role.", "")]
    public Task<ApiResponse<string>> RegisterRoleAsync(CreateOrUpdateRoleRequest request)
    {
        return _roleService.CreateOrUpdateAsync(request);
    }

    [HttpDelete("{id}")]
    [MustHavePermission(OPSAction.Delete, OPSResource.Roles)]
    [OpenApiOperation("Delete a role.", "")]
    public Task<ApiResponse<string>> DeleteAsync(string id)
    {
        return _roleService.DeleteAsync(id);
    }
}