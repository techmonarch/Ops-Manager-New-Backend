namespace OpsManagerAPI.Application.Features.DistributionTransformers.Dtos;
public class DistributionTransformerDetailDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType AssetId { get; set; }
    public DefaultIdType OfficeId { get; set; }
    public string? Name { get; set; }
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
    public DateTime InstallationDate { get; set; }
    public int Capacity { get; set; }
    public string? Status { get; set; }
    public int Rating { get; set; }
    public string? Maker { get; set; }
    public string? FeederPillarType { get; set; }
}
