using Microsoft.AspNetCore.WebUtilities;
using OpsManagerAPI.Application.Common.Exceptions;
using OpsManagerAPI.Application.Common.Mailing;
using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Identity.Users.Password;

namespace OpsManagerAPI.Infrastructure.Identity;
internal partial class UserService
{
    public async Task<ApiResponse<string>> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
    {
        EnsureValidTenant();

        var user = await _userManager.FindByEmailAsync(request.Email.Normalize());
        if (user is null || !await _userManager.IsEmailConfirmedAsync(user))
        {
            // Don't reveal that the user does not exist or is not confirmed
            throw new InternalServerException("An Error has occurred!");
        }

        // For more information on how to enable account confirmation and password reset please
        // visit https://go.microsoft.com/fwlink/?LinkID=532713
        string code = await _userManager.GeneratePasswordResetTokenAsync(user);
        const string route = "account/reset-password";
        var endpointUri = new Uri(string.Concat($"{origin}/", route));
        string passwordResetUrl = QueryHelpers.AddQueryString(endpointUri.ToString(), "Token", code);
        var mailRequest = new MailRequest(
            new List<string> { request.Email },
            "Reset Password",
            $"Your Password Reset Token is '{code}'. You can reset your password using the {passwordResetUrl} Endpoint.");
        await _mailService.SendAsync(mailRequest, CancellationToken.None);

        return new ApiResponse<string>(true, "Password Reset Mail has been sent to your authorized Email.", "Password Reset Mail has been sent to your authorized Email.");
    }

    public async Task<ApiResponse<string>> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email?.Normalize()!);

        // Don't reveal that the user does not exist
        _ = user ?? throw new InternalServerException("An Error has occurred!");

        var result = await _userManager.ResetPasswordAsync(user, request.Token!, request.Password!);

        return new ApiResponse<string>(true, "Operation Successful", result.Succeeded
            ? "Password Reset Successful!"
            : throw new InternalServerException("An Error has occurred!"));
    }

    public async Task ChangePasswordAsync(ChangePasswordRequest model, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new NotFoundException("User Not Found.");

        var result = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);

        if (!result.Succeeded)
        {
            throw new InternalServerException("Change password failed", result.GetErrors());
        }
    }
}