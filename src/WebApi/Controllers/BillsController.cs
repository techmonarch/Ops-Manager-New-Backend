using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.Billings.Dtos;
using OpsManagerAPI.Application.Features.Billings.Queries;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;

public class BillsController : VersionNeutralApiController
{
    [HttpGet]
    [MustHavePermission(OPSAction.View, OPSResource.Billings)]
    [OpenApiOperation("Get Bills History.", "Retrieve a paginated list of Bills")]
    public async Task<ApiResponse<PaginationResponse<BillingDetailsDto>>> GetBillingHistory([FromQuery] GetBillingQueries request)
    {
        return await Mediator.Send(request);
    }
}
