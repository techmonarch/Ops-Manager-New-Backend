using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Finbuckle.MultiTenant;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OpsManagerAPI.Application.Common.Exceptions;
using OpsManagerAPI.Application.Common.FileStorage;
using OpsManagerAPI.Application.Common.Interfaces;
using OpsManagerAPI.Application.Common.Mailing;
using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Common.Specification;
using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Application.Features.Teams.Dtos;
using OpsManagerAPI.Application.Identity.Users;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Infrastructure.Auth;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.Infrastructure.Persistence.Context;

namespace OpsManagerAPI.Infrastructure.Identity;
internal partial class UserService : IUserService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _db;
    private readonly IMailService _mailService;
    private readonly SecuritySettings _securitySettings;
    private readonly IEmailTemplateService _templateService;
    private readonly IFileStorageService _fileStorage;
    private readonly ITenantInfo _currentTenant;
    private readonly ICurrentUser _currentUser;
    private readonly IStaffRepository _staffRepository;

    public UserService(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ApplicationDbContext db,
        IMailService mailService,
        IEmailTemplateService templateService,
        IFileStorageService fileStorage,
        ITenantInfo currentTenant,
        ICurrentUser currentUser,
        IOptions<SecuritySettings> securitySettings,
        IStaffRepository staffRepository)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _db = db;
        _mailService = mailService;
        _templateService = templateService;
        _fileStorage = fileStorage;
        _currentTenant = currentTenant;
        _securitySettings = securitySettings.Value;
        _staffRepository = staffRepository;
        _currentUser = currentUser;
    }

    public async Task<PaginationResponse<UserDetailsDto>> SearchAsync(UserListFilter filter, CancellationToken cancellationToken)
    {
        var spec = new ApplicationUserByPaginationFilterSpec<ApplicationUser>(filter);

        var users = await _userManager.Users
            .WithSpecification(spec)
            .ProjectToType<UserDetailsDto>()
            .ToListAsync(cancellationToken);
        int count = await _userManager.Users
            .CountAsync(cancellationToken);

        return new PaginationResponse<UserDetailsDto>(users, count, filter.PageNumber, filter.PageSize);
    }

    public async Task<bool> ExistsWithNameAsync(string name)
    {
        EnsureValidTenant();
        return await _userManager.FindByNameAsync(name) is not null;
    }

    public async Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null)
    {
        EnsureValidTenant();
        return await _userManager.FindByEmailAsync(email.Normalize()) is ApplicationUser user && user.Id != exceptId;
    }

    public async Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null)
    {
        EnsureValidTenant();
        return await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber) is ApplicationUser user && user.Id != exceptId;
    }

    private void EnsureValidTenant()
    {
        if (string.IsNullOrWhiteSpace(_currentTenant?.Id))
        {
            throw new UnauthorizedException("Invalid Tenant.");
        }
    }

    public async Task<ApiResponse<PaginationResponse<UserDetailsDto>>> GetListAsync(UserListFilter request, CancellationToken cancellationToken)
    {
        return await GetAllUserDetailsAsync(request.Role, request, cancellationToken);
    }

    public async Task<List<string?>> GetAllFcmTokensAsync()
    {
        return await _userManager.Users
            .Where(user => !string.IsNullOrEmpty(user.FCMToken))
            .Select(user => user.FCMToken)
            .ToListAsync();
    }

    public async Task<ApiResponse<int>> GetCountAsync(CancellationToken cancellationToken)
    {
        int response = await _userManager.Users.AsNoTracking().CountAsync(cancellationToken);
        return new ApiResponse<int>(true, "Count retrieved Successfully", response);
    }

    public async Task<ApiResponse<UserDetailsDto>> GetAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException("User Not Found.");

        var result = user.Adapt<UserDetailsDto>();
        var currentUserId = _currentUser.GetUserId();
        var currentStaff = await _staffRepository.GetByUserIdAsync(currentUserId, cancellationToken);

        result.ImageUrl = user.ImageUrl;
        result.State = currentStaff.State;
        result.City = currentStaff.City;
        result.LGA = currentStaff.LGA;
        result.StaffNumber = currentStaff.StaffNumber;
        result.OfficeId = currentStaff.OfficeId.ToString();

        return new ApiResponse<UserDetailsDto>(true, "User retrieved Sucessfully", result);
    }

    public async Task ToggleStatusAsync(ToggleUserStatusRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.Where(u => u.Id == request.UserId).FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException("User Not Found.");

        bool isAdmin = await _userManager.IsInRoleAsync(user, OPSRoles.Admin);
        if (isAdmin)
        {
            throw new ConflictException("Administrators Profile's Status cannot be toggled");
        }

        user.IsActive = request.ActivateUser;

        await _userManager.UpdateAsync(user);
    }

    private async Task<ApiResponse<PaginationResponse<UserDetailsDto>>> GetAllUserDetailsAsync(string? role, UserListFilter request, CancellationToken cancellationToken)
    {
        var spec = new ApplicationUserByPaginationFilterSpec<ApplicationUser>(request);

        // Query users without role filter
        var usersQuery = _userManager.Users.AsNoTracking();

        // Apply additional filtering by Id if provided
        if (request.Id.HasValue)
        {
            usersQuery = usersQuery.Where(user => user.Id == request.Id.ToString());
        }

        var users = await usersQuery.WithSpecification(spec).ToListAsync(cancellationToken);

        // Filter users by role on the client side
        if (!string.IsNullOrEmpty(role))
        {
            var usersInRole = new List<ApplicationUser>();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, role))
                {
                    usersInRole.Add(user);
                }
            }

            users = usersInRole;
        }

        var userIds = users.ConvertAll(user => Guid.Parse(user.Id));

        // Fetch all staff details in one go
        var staffDetails = await _staffRepository.GetByUserIdsAsync(userIds, cancellationToken);

        var clwUserDetails = users.ConvertAll(user =>
        {
            var currentStaff = staffDetails.Find(staff => staff.ApplicationUserId == Guid.Parse(user.Id));
            var result = user.Adapt<UserDetailsDto>();
            result.ImageUrl = user.ImageUrl;
            result.State = currentStaff?.State;
            result.City = currentStaff?.City;
            result.LGA = currentStaff?.LGA;
            result.StaffNumber = currentStaff?.StaffNumber;
            result.OfficeId = currentStaff?.OfficeId.ToString();
            return result;
        });

        var paginatedResponse = new PaginationResponse<UserDetailsDto>(clwUserDetails, clwUserDetails.Count, request.PageNumber, request.PageSize);
        string successMessage = role == null ? "Users Successfully retrieved" : $"{role} Successfully retrieved";
        return new ApiResponse<PaginationResponse<UserDetailsDto>>(true, successMessage, paginatedResponse);
    }

    public List<TeamMemberDetailsDto> GetTeamMemberDetails(Team team)
    {
        var staffMembers = team.StaffTeams.Select(staffTeam =>
        {
            var staff = staffTeam.Staff;

            var userDetails = _userManager.Users.Where(u => u.Id == staff.ApplicationUserId.ToString()).FirstOrDefaultAsync().Result;

            return new TeamMemberDetailsDto
            {
                Id = staffTeam.Id.ToString(),
                StaffNumber = staff.StaffNumber,
                Phone = userDetails.PhoneNumber,
                Name = $"{userDetails.FirstName} {userDetails.LastName}"
            };
        }).ToList();

        return staffMembers;
    }
}