using OpsManagerAPI.Application.Features.MeterReadings.Dtos;

namespace OpsManagerAPI.Application.Features.MeterReadings.Queries;
public interface IMeterReadingQueries : ITransientService
{
    Task<ApiResponse<PaginationResponse<MeterReadingDetailDto>>> SearchAsync(MeterReadingFilterRequest request, CancellationToken cancellationToken);
    Task<ApiResponse<PaginationResponse<MeterReadingDetailDto>>> GetPendingReadinngsAsync(PaginationFilter paginationFilter, CancellationToken cancellationToken);
    Task<ApiResponse<MeterReadingDashboardDto>> Dashboard();
}
