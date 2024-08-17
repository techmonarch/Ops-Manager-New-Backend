using OpsManagerAPI.Application.Features.Teams.Dtos;
using OpsManagerAPI.Application.Identity.Users.Password;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using System.Security.Claims;

namespace OpsManagerAPI.Application.Identity.Users;
public interface IUserService : ITransientService
{
    Task<PaginationResponse<UserDetailsDto>> SearchAsync(UserListFilter filter, CancellationToken cancellationToken);
    Task<List<string?>> GetAllFcmTokensAsync();
    Task<bool> ExistsWithNameAsync(string name);
    Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null);
    Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null);

    Task<ApiResponse<PaginationResponse<UserDetailsDto>>> GetListAsync(UserListFilter request, CancellationToken cancellationToken);

    Task<ApiResponse<int>> GetCountAsync(CancellationToken cancellationToken);

    Task<ApiResponse<UserDetailsDto>> GetAsync(string userId, CancellationToken cancellationToken);

    Task<ApiResponse<List<UserRoleDto>>> GetRolesAsync(string userId, CancellationToken cancellationToken);
    Task<ApiResponse<string>> AssignRolesAsync(string userId, UserRolesRequest request, CancellationToken cancellationToken);

    Task<ApiResponse<List<string>>> GetPermissionsAsync(string userId, CancellationToken cancellationToken);
    Task<bool> HasPermissionAsync(string userId, string permission, CancellationToken cancellationToken = default);

    Task ToggleStatusAsync(ToggleUserStatusRequest request, CancellationToken cancellationToken);

    Task<ApiResponse<string>> GetOrCreateFromPrincipalAsync(ClaimsPrincipal principal);
    Task<ApiResponse<string>> CreateAsync(CreateUserRequest request, string origin);
    Task UpdateAsync(UpdateUserRequest request, string userId);

    Task<ApiResponse<string>> ConfirmEmailAsync(string userId, string code, string disco, CancellationToken cancellationToken);
    Task<ApiResponse<string>> ConfirmPhoneNumberAsync(string userId, string code);

    Task<ApiResponse<string>> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
    Task<ApiResponse<string>> ResetPasswordAsync(ResetPasswordRequest request);
    Task ChangePasswordAsync(ChangePasswordRequest request, string userId);
    List<TeamMemberDetailsDto> GetTeamMemberDetails(Team team);
}