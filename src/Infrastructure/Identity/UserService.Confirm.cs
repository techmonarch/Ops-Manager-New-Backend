using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Exceptions;
using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Infrastructure.Common;
using OpsManagerAPI.Infrastructure.Multitenancy;
using System.Text;

namespace OpsManagerAPI.Infrastructure.Identity;
internal partial class UserService
{
    private async Task<string> GetEmailVerificationUriAsync(ApplicationUser user, string origin)
    {
        EnsureValidTenant();

        string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        const string route = "api/users/confirm-email/";
        var endpointUri = new Uri(string.Concat($"{origin}/", route));
        string verificationUri = QueryHelpers.AddQueryString(endpointUri.ToString(), QueryStringKeys.UserId, user.Id);
        verificationUri = QueryHelpers.AddQueryString(verificationUri, QueryStringKeys.Code, code);
        verificationUri = QueryHelpers.AddQueryString(verificationUri, MultitenancyConstants.TenantIdName, _currentTenant?.Id!);
        return verificationUri;
    }

    public async Task<ApiResponse<string>> ConfirmEmailAsync(string userId, string code, string disco, CancellationToken cancellationToken)
    {
        EnsureValidTenant();

        var user = await _userManager.Users
            .Where(u => u.Id == userId && !u.EmailConfirmed)
            .FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new InternalServerException("An error occurred while confirming E-Mail.");

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);

        return new ApiResponse<string>(true, "Operation Successful", result.Succeeded
            ? string.Format("Account Confirmed for E-Mail {0}. You can now use the /api/tokens endpoint to generate JWT.", user.Email)
            : throw new InternalServerException(string.Format("An error occurred while confirming {0}", user.Email)));
    }

    public async Task<ApiResponse<string>> ConfirmPhoneNumberAsync(string userId, string code)
    {
        EnsureValidTenant();

        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new InternalServerException("An error occurred while confirming Mobile Phone.");
        if (string.IsNullOrEmpty(user.PhoneNumber)) throw new InternalServerException("An error occurred while confirming Mobile Phone.");

        var result = await _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, code);

        return new ApiResponse<string>(true, "Operation Successful", result.Succeeded
            ? user.PhoneNumberConfirmed
                ? string.Format("Account Confirmed for Phone Number {0}. You can now use the /api/tokens endpoint to generate JWT.", user.PhoneNumber)
                : string.Format("Account Confirmed for Phone Number {0}. You should confirm your E-mail before using the /api/tokens endpoint to generate JWT.", user.PhoneNumber)
            : throw new InternalServerException(string.Format("An error occurred while confirming {0}", user.PhoneNumber)));
    }
}
