using Microsoft.AspNetCore.Http;
using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.Evaluations.Commands;

public class CreateEvaluationCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public string? BuildingStatus { get; set; }
    public string? ContactSurname { get; set; }
    public string? ContactFirstname { get; set; }
    public string? ContactPhone { get; set; }
    public string? ContactEmail { get; set; }
    public CustomerType CustomerType { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? LGA { get; set; }
    public string? FeederId { get; set; }
    public string? DssId { get; set; }
    public string? CustomerFirstname { get; set; }
    public string? CustomerLastname { get; set; }
    public string? CustomerMiddlename { get; set; }
    public string? CustomerAddress { get; set; }
    public double CustomerLatitude { get; set; }
    public double CustomerLongitude { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerPhone { get; set; }
    public CustomerStatus CustomerStatus { get; set; }
    public string? BusinessNature { get; set; }
    public string? PremiseUse { get; set; }
    public bool MeterInstalled { get; set; }
    public string? AccountNumber { get; set; }
    public string? MeterNumber { get; set; }
    public double PresentMeterReadingKwh { get; set; }
    public double LastActualReadingKwh { get; set; }
    public double LoadReadingRed { get; set; }
    public double LoadReadingYellow { get; set; }
    public double LoadReadingBlue { get; set; }
    public string? Comment { get; set; }
    public bool CertifiedOk { get; set; }
    public double OverdraftKwh { get; set; }
    public double OverdraftNaira { get; set; }
    public double TariffDifference { get; set; }
    public IFormFile? MeterImage { get; set; }
    public IFormFile? BypassImage { get; set; }
    public IFormFile? MeterVideo { get; set; }
    public IFormFile? CorrectionImage { get; set; }
    public IFormFile? CorrectionVideo { get; set; }
    public string? RpaFinalRecommendation { get; set; }
    public string? ExistingTariffId { get; set; }
    public string? CorrectTariffId { get; set; }
    public string? ExistingServiceBand { get; set; }
    public string? CorrectServiceBand { get; set; }
    public double Last3MonthsConsumption { get; set; }
    public double PowerFactor { get; set; }
    public double LoadFactor { get; set; }
    public double AvailabilityOfSupply { get; set; }
    public double EnergyBilledMonth { get; set; }
    public double EnergyBilledAvg { get; set; }
    public double LrLyLbAvg { get; set; }
    public double UnBilledEnergy { get; set; }
    public int NumOfLorMonth { get; set; }
    public double LorChargedForMeteringIssues { get; set; }
    public bool IsSubsequentOffender { get; set; }
    public double AdministrativeCharge { get; set; }
    public double LorChargedForEnergyTheft { get; set; }
    public double PenaltyChargedForEnergyTheft { get; set; }
    public int NumOfAvailabilityDays { get; set; }
    public string? Landmark { get; set; }
    public string? MeterType { get; set; }
    public string? MeterRating { get; set; }
    public string? ModeOfPayment { get; set; }
    public string? MeterMaker { get; set; }
    public AccountType AccountType { get; set; }
}

public class CreateEvaluationCommandHandler : IRequestHandler<CreateEvaluationCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepositoryWithEvents<Evaluation> _repository;
    private readonly IStaffRepository _staffRepository;
    private readonly IFileStorageService _fileStorage;
    private readonly ICurrentUser _currentUser;

    public CreateEvaluationCommandHandler(IRepositoryWithEvents<Evaluation> repository, ICurrentUser currentUser, IStaffRepository staffRepository, IFileStorageService fileStorage) => (_repository, _currentUser, _staffRepository, _fileStorage) = (repository, currentUser, staffRepository, fileStorage);

    public async Task<ApiResponse<DefaultIdType>> Handle(CreateEvaluationCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser.GetUserId();
        var currentStaff = await _staffRepository.GetByUserIdAsync(currentUserId, cancellationToken);

        // Upload files and save paths
        string? meterImagePath = request.MeterImage != null
            ? await _fileStorage.UploadAsync<string>(request.MeterImage, FileType.Image, cancellationToken)
            : null;

        string? bypassImagePath = request.BypassImage != null
            ? await _fileStorage.UploadAsync<string>(request.BypassImage, FileType.Image, cancellationToken)
            : null;

        string? meterVideoPath = request.MeterVideo != null
            ? await _fileStorage.UploadAsync<string>(request.MeterVideo, FileType.Video, cancellationToken)
            : null;

        string? correctionImagePath = request.CorrectionImage != null
            ? await _fileStorage.UploadAsync<string>(request.CorrectionImage, FileType.Image, cancellationToken)
            : null;

        string? correctionVideoPath = request.CorrectionVideo != null
            ? await _fileStorage.UploadAsync<string>(request.CorrectionVideo, FileType.Video, cancellationToken)
            : null;

        // Create the new evaluation
        var evaluation = new Evaluation(
            currentStaff.Id,
            request.BuildingStatus,
            request.ContactSurname,
            request.ContactFirstname,
            request.ContactPhone,
            request.ContactEmail,
            request.CustomerType,
            request.State,
            request.City,
            request.LGA,
            request.FeederId,
            request.DssId,
            request.CustomerFirstname,
            request.CustomerLastname,
            request.CustomerMiddlename,
            request.CustomerAddress,
            request.CustomerLatitude,
            request.CustomerLongitude,
            request.CustomerEmail,
            request.CustomerPhone,
            request.CustomerStatus,
            request.BusinessNature,
            request.PremiseUse,
            request.MeterInstalled,
            request.AccountNumber,
            request.MeterNumber,
            request.PresentMeterReadingKwh,
            request.LastActualReadingKwh,
            request.LoadReadingRed,
            request.LoadReadingYellow,
            request.LoadReadingBlue,
            request.Comment,
            request.CertifiedOk,
            request.OverdraftKwh,
            request.OverdraftNaira,
            request.TariffDifference,
            meterImagePath,
            bypassImagePath,
            meterVideoPath,
            correctionImagePath,
            correctionVideoPath,
            request.RpaFinalRecommendation,
            request.ExistingTariffId,
            request.CorrectTariffId,
            request.ExistingServiceBand,
            request.CorrectServiceBand,
            request.Last3MonthsConsumption,
            request.PowerFactor,
            request.LoadFactor,
            request.AvailabilityOfSupply,
            request.EnergyBilledMonth,
            request.EnergyBilledAvg,
            request.LrLyLbAvg,
            request.UnBilledEnergy,
            request.NumOfLorMonth,
            request.LorChargedForMeteringIssues,
            request.IsSubsequentOffender,
            request.AdministrativeCharge,
            request.LorChargedForEnergyTheft,
            request.PenaltyChargedForEnergyTheft,
            request.NumOfAvailabilityDays,
            request.Landmark,
            request.MeterType,
            request.MeterRating,
            request.ModeOfPayment,
            request.MeterMaker,
            request.AccountType);

        await _repository.AddAsync(evaluation, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Customer Successfully evaluated", evaluation.Id);
    }
}
