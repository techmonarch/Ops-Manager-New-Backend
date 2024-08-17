using Microsoft.Extensions.Logging;
using OpsManagerAPI.Domain.Aggregates.OfficeAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;

namespace OpsManagerAPI.Infrastructure.Persistence.CustomSeeders;

public class OfficeSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<OfficeSeeder> _logger;

    public OfficeSeeder(ILogger<OfficeSeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.Offices.Any())
        {
            _logger.LogInformation("Started to Seed Offices.");

            var offices = new List<Office>
            {
                new(name: "Main HQ", officeLevelId: _db.OfficeLevels.First(o => o.LevelId == 0).Id ),
                new(name: "State Office 1", officeLevelId: _db.OfficeLevels.First(o => o.LevelId == 1).Id ),
                new(name: "Local Gov Office 1", officeLevelId: _db.OfficeLevels.First(o => o.LevelId == 2).Id ),
                new(name: "City Office 1", officeLevelId: _db.OfficeLevels.First(o => o.LevelId == 3).Id ),
            };

            await _db.Offices.AddRangeAsync(offices, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Offices.");
        }
    }
}
