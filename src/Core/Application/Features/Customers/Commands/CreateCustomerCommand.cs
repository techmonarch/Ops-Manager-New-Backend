using OpsManagerAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsManagerAPI.Application.Features.Customers.Commands;
public class CreateCustomerCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public string? AccountNumber { get;  set; }
    public string? MeterNumber { get;  set; }
    public DefaultIdType TariffId { get; set; }
    public DefaultIdType DistributionTransformerId { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? City { get;  set; }
    public string? State { get; set; }
    public string? LGA { get; set; }
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
    public string? CustomerType { get; set; }
    public string? AccountType { get; set; }
}
