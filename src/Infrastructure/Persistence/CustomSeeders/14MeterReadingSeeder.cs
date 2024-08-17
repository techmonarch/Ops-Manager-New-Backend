using Bogus;
using Microsoft.Extensions.Logging;
using OpsManagerAPI.Domain.Aggregates.MeterAggregate;
using OpsManagerAPI.Domain.Enums;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;

namespace OpsManagerAPI.Infrastructure.Persistence.CustomSeeders;

public class MeterReadingSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<MeterReadingSeeder> _logger;

    public MeterReadingSeeder(ILogger<MeterReadingSeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.MeterReadings.Any())
        {
            _logger.LogInformation("Started to Seed MeterReadings.");

            var staffs = _db.Staffs.ToList();
            var customers = _db.Customers.ToList();
            var transformers = _db.DistributionTransformers.ToList();

            var meterReadingFaker = new Faker<MeterReading>()
                .CustomInstantiator(f => new MeterReading(
                    staffId: f.PickRandom(staffs).Id,
                    previousReading: f.Random.Int(500, 2000),
                    presentReading: f.Random.Int(2001, 4000),
                    consumption: f.Random.Int(1, 100),
                    imagePath: f.Internet.UrlWithPath(),
                    latitude: Convert.ToDecimal(f.Address.Latitude()),
                    longitude: Convert.ToDecimal(f.Address.Longitude()),
                    comment: f.Lorem.Sentence(),
                    meterReadingType: f.PickRandom<MeterReadingType>(),
                    customerId: f.PickRandom(customers).Id,
                    distributionTransformerId: f.PickRandom(transformers)?.Id,
                    isMeterRead: f.Random.Bool()));

            var meterReadings = meterReadingFaker.Generate(10);

            await _db.MeterReadings.AddRangeAsync(meterReadings, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded MeterReadings.");
        }
    }
}
