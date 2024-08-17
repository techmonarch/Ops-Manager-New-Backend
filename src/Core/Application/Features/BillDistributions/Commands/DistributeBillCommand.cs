using OpsManagerAPI.Application.Features.BillDistributions.Specifications;
using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Domain.Aggregates.BillingAggregate;

namespace OpsManagerAPI.Application.Features.BillDistributions.Commands;
public class DistributeBillCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public DefaultIdType CustomerId { get; set; }
    public string? Comment { get; set; }
    public decimal BillAmount { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public bool IsDelivered { get; set; }
}

public class DistributeBillCommandHandler : IRequestHandler<DistributeBillCommand, ApiResponse<DefaultIdType>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<BillDistribution> _repository;
    private readonly IStaffRepository _staffRepository;
    private readonly ICurrentUser _currentUser;

    public DistributeBillCommandHandler(IRepositoryWithEvents<BillDistribution> repository, ICurrentUser currentUser, IStaffRepository staffRepository) => (_repository, _currentUser, _staffRepository) = (repository, currentUser, staffRepository);

    public async Task<ApiResponse<DefaultIdType>> Handle(DistributeBillCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser.GetUserId();
        var currentStaff = await _staffRepository.GetByUserIdAsync(currentUserId, cancellationToken);

        var currentDate = DateTime.Now;

        // Define the start and end dates of the current month
        var startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddSeconds(-1);

        // Check if a bill distribution for the same customer and month already exists
        var existingBillDistribution = await _repository.GetBySpecAsync(new BillDistributionByCustomerAndDateRangeSpec(request.CustomerId, startOfMonth, endOfMonth), cancellationToken);

        if (existingBillDistribution != null)
        {
            return new ApiResponse<DefaultIdType>(false, "Bill distribution for this month already exists.", existingBillDistribution.Id);
        }

        var billDistribution = new BillDistribution(request.CustomerId, currentStaff.Id, request.BillAmount, request.Latitude, request.Longitude, currentDate, request.IsDelivered, request.Comment);

        await _repository.AddAsync(billDistribution, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Bill Successfully distributed", billDistribution.Id);
    }
}
