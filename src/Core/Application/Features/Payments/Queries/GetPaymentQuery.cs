using OpsManagerAPI.Application.Features.Complaints.Dtos;
using OpsManagerAPI.Application.Features.Complaints.Queries;
using OpsManagerAPI.Application.Features.Payments.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsManagerAPI.Application.Features.Payments.Queries;
public class GetPaymentQuery : PaginationFilter, IRequest<ApiResponse<PaginationResponse<PaymentDto>>>
{
    public string? CustomerId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, ApiResponse<PaginationResponse<PaymentDto>>>
{
    private readonly IPaymentRepository _repository;

    public GetPaymentQueryHandler(IPaymentRepository repository) => _repository = repository;

    public async Task<ApiResponse<PaginationResponse<PaymentDto>>> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
    {
        var (payments, totalCount) = await _repository.GetAll(request, cancellationToken);

        // Create the pagination response
        var paginationResponse = new PaginationResponse<PaymentDto>(
            payments,
            totalCount,
            request.PageNumber,
            request.PageSize);

        // Return the API response
        return new ApiResponse<PaginationResponse<PaymentDto>>(true, "Payments history retrieved successfully", paginationResponse);
    }
}