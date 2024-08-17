using OpsManagerAPI.Application.Features.Disconnections.Commands;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using System.Data.Common;

namespace OpsManagerAPI.Application.Features.DisconnectionReasons.Commands;
public class CreateDisconnectionReasonCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public int NumberOfMonth { get; set; }
    public decimal Amount { get; set; }
    public int PercentagePayment { get; set; }
}

public class CreateDisconnectionReasonTicketCommandHandler : IRequestHandler<CreateDisconnectionReasonCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepositoryWithEvents<DisconnectionReason> _repository;

    public CreateDisconnectionReasonTicketCommandHandler(IRepositoryWithEvents<DisconnectionReason> repository) => _repository = repository;

    public async Task<ApiResponse<DefaultIdType>> Handle(CreateDisconnectionReasonCommand request, CancellationToken cancellationToken)
    {
        var disconnections = new DisconnectionReason(request.NumberOfMonth, request.Amount, request.PercentagePayment);

        await _repository.AddAsync(disconnections, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Disconnection Reason Successfully Created", disconnections.Id);
    }
}