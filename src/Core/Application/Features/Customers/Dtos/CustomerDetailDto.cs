using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.Customers.Dtos;
public record CustomerDetailDto(
    string AccountNumber,
    string? MeterNumber,
    string FirstName,
    string LastName,
    string Phone,
    string City,
    string State,
    string ServiceBand,
    string CustomerType,
    string AccountType,
    string Status
) : IDto;
