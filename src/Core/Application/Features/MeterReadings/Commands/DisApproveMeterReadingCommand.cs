using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Domain.Aggregates.MeterAggregate;

namespace OpsManagerAPI.Application.Features.MeterReadings.Commands;
public class DisApproveMeterReadingCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public DefaultIdType MeterReadingId { get; set; } = default!;
}

public class DisApproveMeterReadingCommandHandler : IRequestHandler<DisApproveMeterReadingCommand, ApiResponse<DefaultIdType>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<MeterReading> _repository;
    private readonly IStaffRepository _staffRepository;
    private readonly ICurrentUser _currentUser;

    public DisApproveMeterReadingCommandHandler(IRepositoryWithEvents<MeterReading> repository, ICurrentUser currentUser, IStaffRepository staffRepository) => (_repository, _currentUser, _staffRepository) = (repository, currentUser, staffRepository);

    public async Task<ApiResponse<DefaultIdType>> Handle(DisApproveMeterReadingCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser.GetUserId();
        var currentStaff = await _staffRepository.GetByUserIdAsync(currentUserId, cancellationToken);

        var meterReading = await _repository.GetByIdAsync(request.MeterReadingId, cancellationToken);

        meterReading.DisApprove(currentStaff.Id);

        await _repository.UpdateAsync(meterReading, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Meter Reading dis-approved Successfully.", meterReading.Id);
    }
}