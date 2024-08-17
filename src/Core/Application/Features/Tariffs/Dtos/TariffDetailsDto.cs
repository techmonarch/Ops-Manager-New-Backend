namespace OpsManagerAPI.Application.Features.Tariffs.Dtos;
public class TariffDetailsDto
{
    public string? Id { get; set; }
    public string? UniqueId { get; set; }
    public string? TariffCode { get; set; }
    public int MinimumHours { get; set; }
    public string? TariffRate { get; set; }
    public decimal Amount { get; set; }

    public string? ServiceBandName { get; set; }
}
