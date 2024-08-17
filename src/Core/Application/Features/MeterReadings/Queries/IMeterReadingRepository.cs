using OpsManagerAPI.Application.Features.MeterReadings.Dtos;
using OpsManagerAPI.Domain.Aggregates.MeterAggregate;

namespace OpsManagerAPI.Application.Features.MeterReadings.Queries;

public interface IMeterReadingRepository : ITransientService
{
    Task<List<MeterReading>> SearchAsync(MeterReadingFilterRequest request, CancellationToken cancellationToken);
    Task<List<MeterReading>> GetPendingReadinngsAsync(PaginationFilter paginationFilter, CancellationToken cancellationToken);
}
