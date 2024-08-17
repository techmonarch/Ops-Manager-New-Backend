namespace OpsManagerAPI.Application.Identity.Tokens;

public interface ITokenService : ITransientService
{
    Task<ApiResponse<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress, CancellationToken cancellationToken);

    Task<ApiResponse<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);
}