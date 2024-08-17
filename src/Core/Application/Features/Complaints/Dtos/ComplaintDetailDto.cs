using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.Complaints.Dtos;
public class ComplaintDetailDto : IDto
{
    public DefaultIdType? Id { get; set; }
    public string? Category { get; set; }
    public string? SubCategory { get; set; }
    public string? Comment { get; set; }
    public string? ImagePath { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerAccountNumber { get; set; }
    public string? CustomerPhone { get; set; }
    public string? CustomerAdddress { get; set; }
    public string? DistributionTransformerName { get; set; }
}
