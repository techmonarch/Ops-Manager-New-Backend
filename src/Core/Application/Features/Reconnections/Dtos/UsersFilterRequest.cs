namespace OpsManagerAPI.Application.Features.Reconnections.Dtos;

public class UsersFilterRequest : PaginationFilter
{
    public string? Role { get; set; }
}
