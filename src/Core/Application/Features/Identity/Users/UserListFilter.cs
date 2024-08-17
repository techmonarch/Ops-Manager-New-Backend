namespace OpsManagerAPI.Application.Identity.Users;

public class UserListFilter : PaginationFilter
{
    public bool? IsActive { get; set; }
    public string? Role { get; set; }
    public DefaultIdType? Id { get; set; }
}