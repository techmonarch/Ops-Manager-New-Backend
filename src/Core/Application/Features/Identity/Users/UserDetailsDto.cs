namespace OpsManagerAPI.Application.Identity.Users;

public class UserDetailsDto
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; } = true;
    public bool EmailConfirmed { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ImageUrl { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? LGA { get; set; }
    public string? StaffNumber { get; set; }
    public string? OfficeId { get; set; }
    public string? FCMToken { get; set; }
}