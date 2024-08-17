using Bogus;
using Microsoft.Extensions.Logging;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using OpsManagerAPI.Domain.Enums;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;

namespace OpsManagerAPI.Infrastructure.Persistence.CustomSeeders;

public class CustomerSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<CustomerSeeder> _logger;

    public CustomerSeeder(ILogger<CustomerSeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.Customers.Any())
        {
            _logger.LogInformation("Started to Seed Customers.");

            var tariffs = _db.Tariffs.ToList();
            var distributionTransformers = _db.DistributionTransformers.ToList();

            var customerFaker = new Faker<Customer>()
                .CustomInstantiator(f => new Customer(
                    accountNumber: f.Finance.Account(),
                    meterNumber: f.Random.AlphaNumeric(6),
                    tariffId: f.PickRandom(tariffs).Id,
                    distributionTransformerId: f.PickRandom(distributionTransformers).Id,
                    firstName: f.Name.FirstName(),
                    middleName: f.Name.FirstName(),
                    lastName: f.Name.LastName(),
                    phone: f.Phone.PhoneNumber(),
                    email: f.Internet.Email(),
                    address: f.Address.StreetAddress(),
                    city: f.Address.City(),
                    state: f.Address.State(),
                    lGA: f.Address.City(),
                    longitude: Convert.ToDecimal(f.Address.Longitude()),
                    latitude: Convert.ToDecimal(f.Address.Latitude()),
                    customerType: f.PickRandom<CustomerType>(),
                    accountType: f.PickRandom<AccountType>()));

            var customers = customerFaker.Generate(20);
            await _db.Customers.AddRangeAsync(customers, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Customers.");
        }
    }
}
