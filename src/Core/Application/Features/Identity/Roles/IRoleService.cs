namespace OpsManagerAPI.Application.Identity.Roles;

public interface IRoleService : ITransientService
{
    Task<ApiResponse<List<RoleDto>>> GetListAsync(CancellationToken cancellationToken);

    Task<ApiResponse<int>> GetCountAsync(CancellationToken cancellationToken);

    Task<bool> ExistsAsync(string roleName, string? excludeId);

    Task<ApiResponse<RoleDto>> GetByIdAsync(string id);

    Task<ApiResponse<RoleDto>> GetByIdWithPermissionsAsync(string roleId, CancellationToken cancellationToken);

    Task<ApiResponse<string>> CreateOrUpdateAsync(CreateOrUpdateRoleRequest request);

    Task<ApiResponse<string>> UpdatePermissionsAsync(UpdateRolePermissionsRequest request, CancellationToken cancellationToken);

    Task<ApiResponse<string>> DeleteAsync(string id);
}