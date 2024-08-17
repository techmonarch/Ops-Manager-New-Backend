namespace OpsManagerAPI.Application.Identity.Users;

public class CreateUserRequest
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? StaffNumber { get; set; }
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
    public string LGA { get; set; } = default!;
    public string RoleId { get; set; } = default!;
    public string OfficeName { get; set; } = default!;
}