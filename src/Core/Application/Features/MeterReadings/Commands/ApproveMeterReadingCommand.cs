using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Domain.Aggregates.MeterAggregate;

namespace OpsManagerAPI.Application.Features.MeterReadings.Commands;
public class ApproveMeterReadingCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public DefaultIdType MeterReadingId { get; set; } = default!;
}

public class ApproveMeterReadingCommandHandler : IRequestHandler<ApproveMeterReadingCommand, ApiResponse<DefaultIdType>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<MeterReading> _repository;
    private readonly IStaffRepository _staffRepository;
    private readonly ICurrentUser _currentUser;

    public ApproveMeterReadingCommandHandler(IRepositoryWithEvents<MeterReading> repository, ICurrentUser currentUser, IStaffRepository staffRepository) => (_repository, _currentUser, _staffRepository) = (repository, currentUser, staffRepository);

    public async Task<ApiResponse<DefaultIdType>> Handle(ApproveMeterReadingCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser.GetUserId();
        var currentStaff = await _staffRepository.GetByUserIdAsync(currentUserId, cancellationToken);

        var meterReading = await _repository.GetByIdAsync(request.MeterReadingId, cancellationToken);

        meterReading.Approve(currentStaff.Id);

        await _repository.UpdateAsync(meterReading, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Meter Reading Successfully Approved", meterReading.Id);
    }
}