using Bogus;
using Microsoft.Extensions.Logging;
using OpsManagerAPI.Domain.Aggregates.BillingAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;

namespace OpsManagerAPI.Infrastructure.Persistence.CustomSeeders;

public class BillingDistributionSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<BillingDistributionSeeder> _logger;

    public BillingDistributionSeeder(ILogger<BillingDistributionSeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.BillDistributions.Any())
        {
            _logger.LogInformation("Started to Seed BillingDistribution.");

            var customers = _db.Customers.ToList();
            var staffs = _db.Staffs.ToList();
            var random = new Random();

            var billDistributionFaker = new Faker<BillDistribution>()
                .CustomInstantiator(f => new BillDistribution(
                    customerId: f.PickRandom(customers).Id,
                    staffId: f.PickRandom(staffs).Id,
                    billAmount: f.Finance.Amount(100, 1000),
                    latitude: Convert.ToDecimal(f.Address.Latitude()),
                    longitude: Convert.ToDecimal(f.Address.Longitude()),
                    distributionDate: f.Date.Recent(),
                    isDelivered: f.Random.Bool(),
                    comment: f.Lorem.Sentence()));

            var billDistributions = billDistributionFaker.Generate(10);

            await _db.BillDistributions.AddRangeAsync(billDistributions, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded BillingDistribution.");
        }
    }
}
