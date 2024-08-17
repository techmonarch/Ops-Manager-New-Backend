using Bogus;
using Microsoft.Extensions.Logging;
using OpsManagerAPI.Domain.Aggregates.DownloadsManager;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;

namespace OpsManagerAPI.Infrastructure.Persistence.CustomSeeders;

public class DownloadManagerSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<DownloadManagerSeeder> _logger;

    public DownloadManagerSeeder(ILogger<DownloadManagerSeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.DownloadManagers.Any())
        {
            _logger.LogInformation("Started to Seed DownloadManagers.");

            var rolesList = OPSRoles.DefaultRoles;

            var downloadManagerFaker = new Faker<DownloadManager>()
                .CustomInstantiator(f => new DownloadManager(
                    title: f.Lorem.Sentence(3),
                    description: f.Lorem.Paragraph(),
                    filePath: f.System.FilePath(),
                    roles: f.PickRandom(rolesList, f.Random.Int(1, rolesList.Count)).ToList(),
                    isEnabled: f.Random.Bool()));

            var downloadManagers = downloadManagerFaker.Generate(10);

            await _db.DownloadManagers.AddRangeAsync(downloadManagers, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded DownloadManagers.");
        }
    }
}
