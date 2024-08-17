using Bogus;
using Microsoft.Extensions.Logging;
using OpsManagerAPI.Domain.Aggregates.BillingAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;

namespace OpsManagerAPI.Infrastructure.Persistence.CustomSeeders;

public class BillingSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<BillingSeeder> _logger;

    public BillingSeeder(ILogger<BillingSeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.Billings.Any())
        {
            _logger.LogInformation("Started to Seed Billings.");

            var customers = _db.Customers.ToList();
            var random = new Random();

            var billingFaker = new Faker<Billing>()
                .CustomInstantiator(f => new Billing(
                    billDate: f.Date.Past(1),
                    dueDate: f.Date.Recent(30),
                    consumption: f.Random.Int(100, 1000),
                    arrears: f.Random.Decimal(0, 500),
                    vat: f.Random.Decimal(0, 100),
                    currentCharge: f.Random.Decimal(50, 300),
                    totalCharge: f.Random.Decimal(150, 400),
                    totalDue: f.Random.Decimal(200, 800),
                    customerId: f.PickRandom(customers).Id));

            var billings = billingFaker.Generate(10);

            await _db.Billings.AddRangeAsync(billings, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Billings.");
        }
    }
}
