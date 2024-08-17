using Microsoft.AspNetCore.Http;

namespace OpsManagerAPI.Application.Identity.Users;

public class UpdateUserRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? FCMToken { get; set; }
    public IFormFile? Image { get; set; }
}