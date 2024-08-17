using OpsManagerAPI.Domain.Common;

namespace OpsManagerAPI.Domain.Aggregates.DownloadsManager;
public class DownloadManager : AuditableEntity, IAggregateRoot
{
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string FilePath { get; private set; } = default!;
    public List<string> Roles { get; private set; } = new();
    public bool IsEnabled { get; private set; }

    #region Constructors
    private DownloadManager()
    {
    }

    public DownloadManager(string title, string description, string filePath, List<string> roles, bool isEnabled)
    {
        Title = title;
        Description = description;
        FilePath = filePath;
        Roles = roles;
        IsEnabled = isEnabled;
    }
    #endregion

    #region Behaviours
    public void UpdateAccessibility(List<string> roles)
    {
        Roles = roles;
    }

    public void Disable()
    {
        IsEnabled = false;
    }

    public void Enable()
    {
        IsEnabled = true;
    }

    #endregion
}
