using Microsoft.Extensions.Logging;
using OpsManagerAPI.Domain.Aggregates.OfficeAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;

namespace OpsManagerAPI.Infrastructure.Persistence.CustomSeeders;

public class OfficeLevelSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<OfficeLevelSeeder> _logger;

    public OfficeLevelSeeder(ILogger<OfficeLevelSeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.OfficeLevels.Any())
        {
            _logger.LogInformation("Started to Seed Office Levels.");

            var officeLevels = new List<OfficeLevel>
            {
                new("Headquarters", true, 0),
                new("State Office", true, 1),
                new("Local Government Office", true, 2),
                new("City Office", true, 3),
            };

            await _db.OfficeLevels.AddRangeAsync(officeLevels, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Office Levels.");
        }
    }
}

