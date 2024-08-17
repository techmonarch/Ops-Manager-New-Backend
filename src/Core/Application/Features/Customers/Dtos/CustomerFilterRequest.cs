using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.Customers.Dtos;
public class CustomerFilterRequest : PaginationFilter
{
    public DefaultIdType? Id { get; set; }
    public string? Region { get; set; }
    public string? Hub { get; set; }
    public string? ServiceCenter { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? AccountNumber { get; set; }
    public string? MeterNumber { get; set; }
    public string? Email { get; set; }
    public CustomerStatus? Status { get; set; }
    public AccountType? AccountType { get; set; }
    public CustomerType? CustomerType { get; set; }
    public string? ServiceBand { get; set; }
    public string? NotPaidMonthAgo { get; set; }
    public DateTime? NotPaidBetweenStartDate { get; set; }
    public DateTime? NotPaidBetweenEndDate { get; set; }
    public string? CurrentBillPayment { get; set; }
    public Guid? DistributionTransformerId { get; set; }
}
