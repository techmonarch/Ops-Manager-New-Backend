using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using OpsManagerAPI.Application.Common.Exceptions;
using OpsManagerAPI.Application.Common.Mailing;
using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Identity.Users;
using OpsManagerAPI.Domain.Aggregates.StaffAggregate;
using OpsManagerAPI.Domain.Enums;
using OpsManagerAPI.Domain.Util;
using OpsManagerAPI.Infrastructure.Authorization;
using StackExchange.Redis;
using System.Security.Claims;

namespace OpsManagerAPI.Infrastructure.Identity;
internal partial class UserService
{
    /// <summary>
    /// This is used when authenticating with AzureAd.
    /// The local user is retrieved using the objectidentifier claim present in the ClaimsPrincipal.
    /// If no such claim is found, an InternalServerException is thrown.
    /// If no user is found with that ObjectId, a new one is created and populated with the values from the ClaimsPrincipal.
    /// If a role claim is present in the principal, and the user is not yet in that roll, then the user is added to that role.
    /// </summary>
    public async Task<ApiResponse<string>> GetOrCreateFromPrincipalAsync(ClaimsPrincipal principal)
    {
        string? objectId = principal.GetObjectId();
        if (string.IsNullOrWhiteSpace(objectId))
        {
            throw new InternalServerException("Invalid objectId");
        }

        var user = await _userManager.Users.Where(u => u.ObjectId == objectId).FirstOrDefaultAsync()
            ?? await CreateOrUpdateFromPrincipalAsync(principal);

        if (principal.FindFirstValue(ClaimTypes.Role) is string role &&
            await _roleManager.RoleExistsAsync(role) &&
            !await _userManager.IsInRoleAsync(user, role))
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        return new ApiResponse<string>(true, "Operation Successful", user.Id);
    }

    private async Task<ApplicationUser> CreateOrUpdateFromPrincipalAsync(ClaimsPrincipal principal)
    {
        string? email = principal.FindFirstValue(ClaimTypes.Upn);
        string? username = principal.GetDisplayName();
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username))
        {
            throw new InternalServerException(string.Format("Username or Email not valid."));
        }

        var user = await _userManager.FindByNameAsync(username);
        if (user is not null && !string.IsNullOrWhiteSpace(user.ObjectId))
        {
            throw new InternalServerException(string.Format("Username {0} is already taken.", username));
        }

        if (user is null)
        {
            user = await _userManager.FindByEmailAsync(email);
            if (user is not null && !string.IsNullOrWhiteSpace(user.ObjectId))
            {
                throw new InternalServerException(string.Format("Email {0} is already taken.", email));
            }
        }

        IdentityResult? result;
        if (user is not null)
        {
            user.ObjectId = principal.GetObjectId();
            result = await _userManager.UpdateAsync(user);
        }
        else
        {
            user = new ApplicationUser
            {
                ObjectId = principal.GetObjectId(),
                FirstName = principal.FindFirstValue(ClaimTypes.GivenName),
                LastName = principal.FindFirstValue(ClaimTypes.Surname),
                Email = email,
                NormalizedEmail = email.ToUpperInvariant(),
                UserName = username,
                NormalizedUserName = username.ToUpperInvariant(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };
            result = await _userManager.CreateAsync(user);
        }

        if (!result.Succeeded)
        {
            throw new InternalServerException("Validation Errors Occurred.", result.GetErrors());
        }

        return user;
    }

    public async Task<ApiResponse<string>> CreateAsync(CreateUserRequest request, string origin)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId) ?? throw new BadHttpRequestException($"Role with id '{request.RoleId}' does not exist.");

        var office = await _db.Offices.FirstOrDefaultAsync(o => o.Name.Equals(request.OfficeName)) ?? throw new BadHttpRequestException($"Office with name '{request.OfficeName}' does not exist.");

        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) throw new InternalServerException("Validation Errors Occurred.", result.GetErrors());

        string uniqueStaffId = string.IsNullOrWhiteSpace(request.StaffNumber) ? StaffIdGenerator.GenerateUniqueId(user.FirstName, user.LastName) : request.StaffNumber;

        var staff = new Staff(office.Id, Guid.Parse(user.Id), request.City, request.State, request.LGA, uniqueStaffId);
        await _db.Staffs.AddAsync(staff);
        await _db.SaveChangesAsync();

        await _userManager.AddToRoleAsync(user, role.Name!);

        var messages = new List<string> { string.Format("User {0} Registered.", user.UserName) };

        string emailVerificationUri = await GetEmailVerificationUriAsync(user, origin);
        if (_securitySettings.RequireConfirmedAccount && !string.IsNullOrEmpty(user.Email))
        {
            // send verification email
            RegisterUserEmailModel eMailModel = new RegisterUserEmailModel()
            {
                Email = user.Email,
                UserName = user.UserName,
                Url = emailVerificationUri
            };
            var mailRequest = new MailRequest(
                new List<string> { user.Email },
                "Confirm Registration",
                _templateService.GenerateEmailTemplate("email-confirmation", eMailModel));
            await _mailService.SendAsync(mailRequest, CancellationToken.None);
            messages.Add($"Please visit {emailVerificationUri} to verify your account!");
        }

        return new ApiResponse<string>(true, string.Join(Environment.NewLine, messages), emailVerificationUri);
    }

    public async Task UpdateAsync(UpdateUserRequest request, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new NotFoundException("User Not Found.");

        if (request.Image != null)
            user.ImageUrl = await _fileStorage.UploadAsync<string>(request.Image, FileType.Image);

        user.FirstName = request.FirstName ?? user.FirstName;
        user.LastName = request.LastName ?? user.LastName;
        user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
        user.FCMToken = request.FCMToken ?? user.FCMToken;
        string? phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        if (request.PhoneNumber != phoneNumber)
        {
            await _userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
        }

        var result = await _userManager.UpdateAsync(user);

        await _signInManager.RefreshSignInAsync(user);

        if (!result.Succeeded)
        {
            throw new InternalServerException("Update profile failed", result.GetErrors());
        }
    }
}
