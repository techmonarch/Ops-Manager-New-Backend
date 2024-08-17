namespace OpsManagerAPI.Application.Features.Complaints.Dtos;

public class ComplaintCategoryDetailDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string? Name { get; set; }
}