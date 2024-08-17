using Bogus;
using Microsoft.Extensions.Logging;
using OpsManagerAPI.Domain.Aggregates.StaffAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;

namespace OpsManagerAPI.Infrastructure.Persistence.CustomSeeders;

public class StaffSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<StaffSeeder> _logger;

    public StaffSeeder(ILogger<StaffSeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.Staffs.Any())
        {
            _logger.LogInformation("Started to Seed Staffs.");

            var offices = _db.Offices.ToList();

            var staffFaker = new Faker<Staff>()
                .CustomInstantiator(f => new Staff(
                    officeId: f.PickRandom(offices)?.Id ?? default,
                    applicationUserId: Guid.NewGuid(),
                    city: f.Address.City(),
                    state: f.Address.State(),
                    lGA: f.Address.County(),
                    uniqueStaffId: $"STAFF{f.IndexFaker + 1:000}",
                    isSuperAdmin: f.Random.Bool()));

            var staffs = staffFaker.Generate(10);

            await _db.Staffs.AddRangeAsync(staffs, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Staffs.");
        }
    }
}
