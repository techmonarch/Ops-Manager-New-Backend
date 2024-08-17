namespace OpsManagerAPI.Application.Identity.Tokens;

public record RefreshTokenRequest(string Token, string RefreshToken);