using OpsManagerAPI.Application.Features.DistributionTransformers.Dtos;

namespace OpsManagerAPI.Application.Features.DistributionTransformers.Queries;
public class GetDistributionTransformersQueries : PaginationFilter, IRequest<ApiResponse<PaginationResponse<DistributionTransformerDetailDto>>>
{
    public DefaultIdType? Id { get; set; }
    public DefaultIdType? OfficeId { get; set; }
}

public class GetDistributionTransformersQueriesHandler : IRequestHandler<GetDistributionTransformersQueries, ApiResponse<PaginationResponse<DistributionTransformerDetailDto>>>
{
    #region Constructor
    private readonly IDistributionTransformersRepository _repository;

    public GetDistributionTransformersQueriesHandler(IDistributionTransformersRepository repository) => _repository = repository;
    #endregion

    public async Task<ApiResponse<PaginationResponse<DistributionTransformerDetailDto>>> Handle(GetDistributionTransformersQueries request, CancellationToken cancellationToken)
    {
        var response = await _repository.GetAll(request, cancellationToken);

        var paginatedResponse = new PaginationResponse<DistributionTransformerDetailDto>(response, response.Count, request.PageNumber, request.PageSize);

        return new ApiResponse<PaginationResponse<DistributionTransformerDetailDto>>(true, "Distribution Transformers successfully retrieved", paginatedResponse);
    }
}