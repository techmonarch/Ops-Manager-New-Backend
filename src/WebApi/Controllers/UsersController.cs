using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Identity.Users;
using OpsManagerAPI.Application.Identity.Users.Password;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;
public class UsersController : VersionNeutralApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    [MustHavePermission(OPSAction.View, OPSResource.Users)]
    [OpenApiOperation("Get list of all users.", "")]
    public Task<ApiResponse<PaginationResponse<UserDetailsDto>>> GetListAsync([FromQuery] UserListFilter request, CancellationToken cancellationToken)
    {
        return _userService.GetListAsync(request, cancellationToken);
    }

    [HttpGet("{id}")]
    [MustHavePermission(OPSAction.View, OPSResource.Users)]
    [OpenApiOperation("Get a user's details.", "")]
    public Task<ApiResponse<UserDetailsDto>> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return _userService.GetAsync(id, cancellationToken);
    }

    [HttpGet("{id}/roles")]
    [MustHavePermission(OPSAction.View, OPSResource.UserRoles)]
    [OpenApiOperation("Get a user's roles.", "")]
    public Task<ApiResponse<List<UserRoleDto>>> GetRolesAsync(string id, CancellationToken cancellationToken)
    {
        return _userService.GetRolesAsync(id, cancellationToken);
    }

    [HttpPost("{id}/roles")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Register))]
    [MustHavePermission(OPSAction.Update, OPSResource.UserRoles)]
    [OpenApiOperation("Update a user's assigned roles.", "")]
    public Task<ApiResponse<string>> AssignRolesAsync(string id, UserRolesRequest request, CancellationToken cancellationToken)
    {
        return _userService.AssignRolesAsync(id, request, cancellationToken);
    }

    [HttpPost]
    [MustHavePermission(OPSAction.Create, OPSResource.Users)]
    [OpenApiOperation("Creates a new user.", "")]
    public Task<ApiResponse<string>> CreateAsync(CreateUserRequest request)
    {
        return _userService.CreateAsync(request, GetOriginFromRequest());
    }

    [HttpPost("{id}/toggle-status")]
    [MustHavePermission(OPSAction.Update, OPSResource.Users)]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Register))]
    [OpenApiOperation("Toggle a user's active status.", "")]
    public async Task<ActionResult> ToggleStatusAsync(string id, ToggleUserStatusRequest request, CancellationToken cancellationToken)
    {
        if (id != request.UserId)
        {
            return BadRequest();
        }

        await _userService.ToggleStatusAsync(request, cancellationToken);
        return Ok();
    }

    [HttpGet("confirm-email")]
    [AllowAnonymous]
    [OpenApiOperation("Confirm email address for a user.", "")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Search))]
    public Task<ApiResponse<string>> ConfirmEmailAsync([FromQuery] string disco, [FromQuery] string userId, [FromQuery] string code, CancellationToken cancellationToken)
    {
        return _userService.ConfirmEmailAsync(userId, code, disco, cancellationToken);
    }

    [HttpGet("confirm-phone-number")]
    [AllowAnonymous]
    [OpenApiOperation("Confirm phone number for a user.", "")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Search))]
    public Task<ApiResponse<string>> ConfirmPhoneNumberAsync([FromQuery] string userId, [FromQuery] string code)
    {
        return _userService.ConfirmPhoneNumberAsync(userId, code);
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Request a password reset email for a user.", "")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Register))]
    public Task<ApiResponse<string>> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        return _userService.ForgotPasswordAsync(request, GetOriginFromRequest());
    }

    [HttpPost("reset-password")]
    [OpenApiOperation("Reset a user's password.", "")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Register))]
    public Task<ApiResponse<string>> ResetPasswordAsync(ResetPasswordRequest request)
    {
        return _userService.ResetPasswordAsync(request);
    }

    private string GetOriginFromRequest() => $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}";
}
