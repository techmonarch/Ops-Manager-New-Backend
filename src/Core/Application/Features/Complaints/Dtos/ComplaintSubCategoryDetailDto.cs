namespace OpsManagerAPI.Application.Features.Complaints.Dtos;

public class ComplaintSubCategoryDetailDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string? Name { get; set; }
    public DefaultIdType CategoryId { get; set; }
}