using Mapster;
using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Features.Tariffs.Dtos;
using OpsManagerAPI.Application.Features.Tariffs.Queries;
using OpsManagerAPI.Infrastructure.Persistence.Context;

namespace OpsManagerAPI.Infrastructure.Persistence.Repository.EfCoreRepositories;
public class TariffRepository : ITariffRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TariffRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<TariffDetailsDto>> GetAll(CancellationToken cancellationToken)
        => await _dbContext.Tariffs.AsNoTracking().ProjectToType<TariffDetailsDto>().ToListAsync(cancellationToken: cancellationToken);
}
