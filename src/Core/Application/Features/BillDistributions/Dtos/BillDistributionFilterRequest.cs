namespace OpsManagerAPI.Application.Features.BillDistributions.Dtos;

public class BillDistributionFilterRequest : PaginationFilter
{
    public string? CreatedBy { get; set; }
    public bool? ApprovalStatus { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}


