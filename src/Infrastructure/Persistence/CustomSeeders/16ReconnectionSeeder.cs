using Bogus;
using Microsoft.Extensions.Logging;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;

namespace OpsManagerAPI.Infrastructure.Persistence.CustomSeeders;

public class ReconnectionSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<ReconnectionSeeder> _logger;

    public ReconnectionSeeder(ILogger<ReconnectionSeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.Reconnections.Any())
        {
            _logger.LogInformation("Started to Seed Reconnections.");

            var customers = _db.Customers.ToList();
            var staffs = _db.Staffs.ToList();

            var reconnectionFaker = new Faker<Reconnection>()
                .CustomInstantiator(f => new Reconnection(
                    customerId: f.PickRandom(customers)?.Id ?? default,
                    staffId: f.PickRandom(staffs)?.Id ?? default,
                    reason: f.PickRandom(new[] { "Payment received", "Issue resolved", "Customer request", "Technical issue fixed" }),
                    reportedBy: f.PickRandom(staffs)?.Id ?? default,
                    imagePath: null,
                    latitude: Convert.ToDecimal(f.Address.Latitude()),
                    longitude: Convert.ToDecimal(f.Address.Longitude()),
                    comment: f.Lorem.Sentence()));

            var reconnections = reconnectionFaker.Generate(10);

            await _db.Reconnections.AddRangeAsync(reconnections, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Reconnections.");
        }
    }
}
