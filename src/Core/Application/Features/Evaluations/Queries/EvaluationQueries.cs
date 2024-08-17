using OpsManagerAPI.Application.Features.Evaluations.Dtos;

namespace OpsManagerAPI.Application.Features.Evaluations.Queries;
public class EvaluationQueries : IEvaluationQueries
{
    #region Constructor
    private readonly IEvaluationRepository _repository;
    private readonly IDapperRepository _dapperRepository;
    public EvaluationQueries(IEvaluationRepository repository, IDapperRepository dapperRepository)
        => (_repository, _dapperRepository) = (repository, dapperRepository);
    #endregion

    #region EF-Core
    public async Task<ApiResponse<PaginationResponse<EvaluationDetailDto>>> SearchAsync(EvaluationFilterRequest request, CancellationToken cancellationToken)
    {
        var evaluations = await _repository.SearchAsync(request, cancellationToken);

        // Check if there are no evaluations
        if (evaluations == null || evaluations.Count == 0)
        {
            var emptyPaginationResponse = new PaginationResponse<EvaluationDetailDto>(
                new List<EvaluationDetailDto>(),
                0,
                request.PageNumber,
                request.PageSize);

            return new ApiResponse<PaginationResponse<EvaluationDetailDto>>(
                true,
                "No evaluations found",
                emptyPaginationResponse);
        }

        // Map evaluations to EvaluationDetailDto
        var evaluationDataList = evaluations.ConvertAll(evaluation =>
        {
            return new EvaluationDetailDto(
                StaffId: evaluation.StaffId,
                BuildingStatus: evaluation.BuildingStatus,
                ContactSurname: evaluation.ContactSurname,
                ContactFirstname: evaluation.ContactFirstname,
                ContactPhone: evaluation.ContactPhone,
                ContactEmail: evaluation.ContactEmail,
                CustomerType: evaluation.CustomerType.ToString(),
                State: evaluation.State,
                City: evaluation.City,
                LGA: evaluation.LGA,
                FeederId: evaluation.FeederId,
                DssId: evaluation.DssId,
                CustomerFirstname: evaluation.CustomerFirstname,
                CustomerLastname: evaluation.CustomerLastname,
                CustomerMiddlename: evaluation.CustomerMiddlename,
                CustomerAddress: evaluation.CustomerAddress,
                CustomerLatitude: evaluation.CustomerLatitude,
                CustomerLongitude: evaluation.CustomerLongitude,
                CustomerEmail: evaluation.CustomerEmail,
                CustomerPhone: evaluation.CustomerPhone,
                CustomerStatus: evaluation.CustomerStatus.ToString(),
                BusinessNature: evaluation.BusinessNature,
                PremiseUse: evaluation.PremiseUse,
                MeterInstalled: evaluation.MeterInstalled,
                AccountNumber: evaluation.AccountNumber,
                MeterNumber: evaluation.MeterNumber,
                PresentMeterReadingKwh: evaluation.PresentMeterReadingKwh,
                LastActualReadingKwh: evaluation.LastActualReadingKwh,
                LoadReadingRed: evaluation.LoadReadingRed,
                LoadReadingYellow: evaluation.LoadReadingYellow,
                LoadReadingBlue: evaluation.LoadReadingBlue,
                Comment: evaluation.Comment,
                CertifiedOk: evaluation.CertifiedOk,
                OverdraftKwh: evaluation.OverdraftKwh,
                OverdraftNaira: evaluation.OverdraftNaira,
                TariffDifference: evaluation.TariffDifference,
                MeterImage: evaluation.MeterImage,
                BypassImage: evaluation.BypassImage,
                MeterVideo: evaluation.MeterVideo,
                CorrectionImage: evaluation.CorrectionImage,
                CorrectionVideo: evaluation.CorrectionVideo,
                RpaFinalRecommendation: evaluation.RpaFinalRecommendation,
                ExistingTariffId: evaluation.ExistingTariffId,
                CorrectTariffId: evaluation.CorrectTariffId,
                ExistingServiceBand: evaluation.ExistingServiceBand,
                CorrectServiceBand: evaluation.CorrectServiceBand,
                Last3MonthsConsumption: evaluation.Last3MonthsConsumption,
                PowerFactor: evaluation.PowerFactor,
                LoadFactor: evaluation.LoadFactor,
                AvailabilityOfSupply: evaluation.AvailabilityOfSupply,
                EnergyBilledMonth: evaluation.EnergyBilledMonth,
                EnergyBilledAvg: evaluation.EnergyBilledAvg,
                LrLyLbAvg: evaluation.LrLyLbAvg,
                UnBilledEnergy: evaluation.UnBilledEnergy,
                NumOfLorMonth: evaluation.NumOfLorMonth,
                LorChargedForMeteringIssues: evaluation.LorChargedForMeteringIssues,
                IsSubsequentOffender: evaluation.IsSubsequentOffender,
                AdministrativeCharge: evaluation.AdministrativeCharge,
                LorChargedForEnergyTheft: evaluation.LorChargedForEnergyTheft,
                PenaltyChargedForEnergyTheft: evaluation.PenaltyChargedForEnergyTheft,
                NumOfAvailabilityDays: evaluation.NumOfAvailabilityDays,
                Landmark: evaluation.Landmark,
                MeterType: evaluation.MeterType,
                MeterRating: evaluation.MeterRating,
                ModeOfPayment: evaluation.ModeOfPayment,
                MeterMaker: evaluation.MeterMaker,
                AccountType: evaluation.AccountType.ToString());
        });

        // Create the pagination response
        var paginationResponse = new PaginationResponse<EvaluationDetailDto>(
            evaluationDataList,
            evaluationDataList.Count,
            request.PageNumber,
            request.PageSize);

        // Return the API response
        return new ApiResponse<PaginationResponse<EvaluationDetailDto>>(
            true,
            "Evaluations retrieved successfully",
            paginationResponse);
    }

    #endregion

    #region Dapper
    public async Task<ApiResponse<EvaluationDashboardDto>> Dashboard()
    {
        var currentDate = DateTime.Now.Date;
        const string sql = @"
                        DECLARE @TotalCustomersCount INT;
                        DECLARE @EvaluatedCustomersCount INT;
                        
                        SELECT @TotalCustomersCount = COUNT(DISTINCT c.Id) 
                        FROM [Customers] c;

                        SELECT @EvaluatedCustomersCount = COUNT(DISTINCT ev.CustomerId) 
                        FROM [Evaluations] ev;

                        SELECT 
                            @TotalCustomersCount AS TotalCustomersCount,
                            @EvaluatedCustomersCount AS EvaluatedCustomersCount,

                            (SELECT COUNT(DISTINCT ev.CustomerId) 
                                FROM [Evaluations] ev
                                WHERE ev.BypassImage != null) AS BypassCustomersCount,

                            (@TotalCustomersCount - @EvaluatedCustomersCount) AS PendingEvaluationCount,

                            (SELECT COUNT(*) 
                                FROM [Evaluations] ev
                                WHERE CONVERT(DATE, ev.CreatedOn) = @CurrentDate) AS TodayCount";

        var parameters = new { CurrentDate = currentDate };

        var dashboardData = await _dapperRepository.QueryFirstOrDefaultAsync<EvaluationDashboardDto>(sql, parameters);

        if (dashboardData == null)
        {
            return new ApiResponse<EvaluationDashboardDto>(false, "Evaluation dashboard data not found", default!);
        }

        return new ApiResponse<EvaluationDashboardDto>(true, "Evaluation dashboard data retrieved successfully", dashboardData);
    }
    #endregion
}
