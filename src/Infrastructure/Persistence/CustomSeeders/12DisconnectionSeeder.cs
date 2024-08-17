using Bogus;
using Microsoft.Extensions.Logging;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;

namespace OpsManagerAPI.Infrastructure.Persistence.CustomSeeders;

public class DisconnectionSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<DisconnectionSeeder> _logger;

    public DisconnectionSeeder(ILogger<DisconnectionSeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.Disconnections.Any())
        {
            _logger.LogInformation("Started to Seed Disconnections.");

            var customers = _db.Customers.ToList();
            var staffs = _db.Staffs.ToList();

            var disconnectionFaker = new Faker<Disconnection>()
                .CustomInstantiator(f => new Disconnection(
                    customerId: f.PickRandom(customers).Id,
                    staffId: f.PickRandom(staffs).Id,
                    amountOwed: f.Finance.Amount(100, 1000),
                    amountToPay: f.Finance.Amount(50, 500),
                    reason: f.PickRandom(new[] { "Non-payment", "Unauthorized connection", "Meter tampering" }),
                    reportedBy: f.PickRandom(staffs).Id,
                    imagePath: null,
                    comment: f.Lorem.Sentence(),
                    latitude: Convert.ToDecimal(f.Address.Latitude()),
                    longitude: Convert.ToDecimal(f.Address.Longitude())));

            var disconnections = disconnectionFaker.Generate(10);

            await _db.Disconnections.AddRangeAsync(disconnections, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Disconnections.");
        }
    }
}