using Microsoft.AspNetCore.Identity;

namespace OpsManagerAPI.Infrastructure.Identity;
public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? MiddleName { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    public string? ObjectId { get; set; }
    public string? FCMToken { get; set; }
}