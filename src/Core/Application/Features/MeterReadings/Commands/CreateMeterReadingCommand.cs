using Microsoft.AspNetCore.Http;
using OpsManagerAPI.Application.Features.MeterReadings.Specifications;
using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Domain.Aggregates.MeterAggregate;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.MeterReadings.Commands;
public class CreateMeterReadingCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public DefaultIdType? CustomerId { get; set; }
    public DefaultIdType? DistributionTransformerId { get; set; }
    public decimal PresentReading { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public bool IsMeterRead { get; set; }
    public string? Comments { get; set; }
    public IFormFile? Image { get; set; }
    public MeterReadingType MeterReadingType { get; set; }
}

public class CreateMeterReadingCommandHandler : IRequestHandler<CreateMeterReadingCommand, ApiResponse<DefaultIdType>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<MeterReading> _repository;
    private readonly IStaffRepository _staffRepository;
    private readonly IFileStorageService _fileStorage;
    private readonly ICurrentUser _currentUser;

    public CreateMeterReadingCommandHandler(IRepositoryWithEvents<MeterReading> repository, ICurrentUser currentUser, IStaffRepository staffRepository, IFileStorageService fileStorage) => (_repository, _currentUser, _staffRepository, _fileStorage) = (repository, currentUser, staffRepository, fileStorage);

    public async Task<ApiResponse<DefaultIdType>> Handle(CreateMeterReadingCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser?.GetUserId() ?? throw new InvalidOperationException("Current user not found.");
        var currentStaff = await _staffRepository.GetByUserIdAsync(currentUserId, cancellationToken)
            ?? throw new InvalidOperationException("Staff not found for current user.");

        // Define the start and end dates of the current month
        var currentDate = DateTime.Now;
        var startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddSeconds(-1);

        // Check if a meter reading for the same customer/transformer and month already exists
        IMeterReadingSpecification existingMeterReadingSpec = request.MeterReadingType switch
        {
            MeterReadingType.Customer => new MeterReadingByCustomerIdAndDateRangeSpec(request.CustomerId, startOfMonth, endOfMonth),
            MeterReadingType.DistributionTransformer => new MeterReadingByDistributionTransformerIdAndDateRangeSpec(request.DistributionTransformerId, startOfMonth, endOfMonth),
            _ => throw new ArgumentException("Invalid MeterReadingType."),
        };

        var existingMeterReading = await _repository.GetBySpecAsync(existingMeterReadingSpec, cancellationToken);

        if (existingMeterReading != null)
        {
            return new ApiResponse<DefaultIdType>(false, "Meter reading for this month already exists.", existingMeterReading.Id);
        }

        // Calculate the consumption based on the previous reading
        decimal previousReadingValue = existingMeterReading?.PresentReading ?? 0m;
        decimal consumption = request.PresentReading - previousReadingValue;

        // Upload the image and create the new meter reading
        string imagePath = await _fileStorage.UploadAsync<string>(request.Image, FileType.Image, cancellationToken);

        var meterReading = new MeterReading(
            currentStaff.Id,
            previousReadingValue,
            request.PresentReading,
            consumption,
            imagePath,
            request.Latitude,
            request.Longitude,
            request.Comments,
            request.MeterReadingType,
            request.MeterReadingType == MeterReadingType.Customer ? request.CustomerId : null,
            request.MeterReadingType == MeterReadingType.DistributionTransformer ? request.DistributionTransformerId : null,
            request.IsMeterRead);

        await _repository.AddAsync(meterReading, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Meter Reading Successfully Submitted", meterReading.Id);
    }
}