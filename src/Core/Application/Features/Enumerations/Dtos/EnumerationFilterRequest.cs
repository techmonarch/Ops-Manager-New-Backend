namespace OpsManagerAPI.Application.Features.Enumerations.Dtos;

public class EnumerationFilterRequest : PaginationFilter
{
    public string? CreatedBy { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? AccountNumber { get; set; }
    public DefaultIdType? Id { get; set; }
}