namespace OpsManagerAPI.Application.Features.Teams.Dtos;

public class TeamFilterRequest : PaginationFilter
{
    public DefaultIdType? Id { get; set; }
    public string? Name { get; set; }
}