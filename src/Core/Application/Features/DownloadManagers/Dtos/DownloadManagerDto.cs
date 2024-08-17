namespace OpsManagerAPI.Application.Features.DownloadsManagers.Dtos;

public class DownloadManagerDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? FilePath { get; set; }
    public List<string> Roles { get; set; } = new();
    public bool IsEnabled { get; set; }
}