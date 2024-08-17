using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.Payments.Dtos;
using OpsManagerAPI.Application.Features.Payments.Queries;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;

public class PaymentController : VersionNeutralApiController
{
    [HttpGet]
    [MustHavePermission(OPSAction.View, OPSResource.Payments)]
    [OpenApiOperation("Get Payment History.", "Retrieve a paginated list of Payment.")]
    public async Task<ApiResponse<PaginationResponse<PaymentDto>>> GetPaymentHistory([FromQuery] GetPaymentQuery request)
    {
         return await Mediator.Send(request);
    }
}
