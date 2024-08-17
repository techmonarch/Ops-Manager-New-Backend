namespace OpsManagerAPI.Application.Features.Disconnections.Dtos;
public class DisconnectionCustomersDetailDto
{
    public string? AccountNumber { get; set; }
    public string? MeterNumber { get; set; }
    public string? Tariff { get; set; }
    public string? DistributionTransformer { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? LGA { get; set; }
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
    public string? CustomerType { get; set; }
    public string? AccountType { get; set; }
    public string? DisconnectionStatus { get; set; }
    public string? Reason { get; set; }
    public DateTime? DateLogged { get; set; }
    public DateTime? DateApproved { get; set; }
    public DateTime? DateDisconnected { get; set; }
    public Guid? DisconnectionId { get; set; }
    public decimal AmountOwed { get; set; }
    public decimal AmountToPay { get; set; }

}
