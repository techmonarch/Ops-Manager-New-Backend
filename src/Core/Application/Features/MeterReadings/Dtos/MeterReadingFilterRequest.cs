using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.MeterReadings.Dtos;

public class MeterReadingFilterRequest : PaginationFilter
{
    public string? CreatedBy { get; set; }
    public bool? ApprovalStatus { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public MeterReadingType? MeterReadingType { get; set; }
}
