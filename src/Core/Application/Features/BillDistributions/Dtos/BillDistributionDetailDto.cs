namespace OpsManagerAPI.Application.Features.BillDistributions.Dtos;

public record BillDistributionDetailDto(
    string FirstName,
    string LastName,
    string AccountNumber,
    string MeterNumber,
    string Tariff,
    string DSSName,
    decimal BillAmount,
    decimal Latitude,
    decimal Longitude,
    DateTime? DistributionDate,
    bool IsDelivered
);
