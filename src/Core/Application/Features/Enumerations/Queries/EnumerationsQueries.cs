using OpsManagerAPI.Application.Features.Enumerations.Dtos;

namespace OpsManagerAPI.Application.Features.Enumerations.Queries;
public class EnumerationsQueries : IEnumerationsQueries
{
    #region Constructor
    private readonly IEnumerationRepository _repository;
    private readonly IDapperRepository _dapperRepository;
    public EnumerationsQueries(IEnumerationRepository repository, IDapperRepository dapperRepository)
        => (_repository, _dapperRepository) = (repository, dapperRepository);
    #endregion

    #region EF-Core
    public async Task<ApiResponse<PaginationResponse<EnumerationDetailDto>>> SearchAsync(EnumerationFilterRequest request, CancellationToken cancellationToken)
    {
        var enumerations = await _repository.SearchAsync(request, cancellationToken);

        // Check if there are no enumerations
        if (enumerations == null || enumerations.Count == 0)
        {
            var emptyPaginationResponse = new PaginationResponse<EnumerationDetailDto>(
                new List<EnumerationDetailDto>(),
                0,
                request.PageNumber,
                request.PageSize);

            return new ApiResponse<PaginationResponse<EnumerationDetailDto>>(
                true,
                "No enumerations found",
                emptyPaginationResponse);
        }

        // Map enumerations to EnumerationDetailDto
        var enumerationDataList = enumerations.ConvertAll(enumeration =>
        {
            return new EnumerationDetailDto(
                                            Id: enumeration.Id,
                                            FirstName: enumeration.FirstName,
                                            LastName: enumeration.LastName,
                                            AccountNumber: enumeration.AccountNumber,
                                            Tariff: enumeration.Tariff?.UniqueId ?? string.Empty,
                                            Phone: enumeration.Phone,
                                            Email: enumeration.Email,
                                            City: enumeration.City,
                                            LGA: enumeration.LGA,
                                            State: enumeration.State,
                                            PropertyUse: enumeration.BuildingDescription,
                                            MeterNumber: enumeration.MeterNumber!,
                                            DistributionTransformerId: enumeration.DistributionTransformerId,
                                            ContactFirstName: enumeration.ContactFirstName,
                                            MiddleName: enumeration.MiddleName,
                                            ContactLastName: enumeration.ContactLastName,
                                            Gender: enumeration.Gender,
                                            ContactPhone: enumeration.ContactPhone,
                                            ContactEmail: enumeration.ContactEmail,
                                            Address: enumeration.Address,
                                            BuildingDescription: enumeration.BuildingDescription,
                                            Landmark: enumeration.Landmark,
                                            BusinessType: enumeration.BusinessType,
                                            PremiseType: enumeration.PremiseType,
                                            ServiceBand: enumeration.ServiceBand,
                                            Longitude: enumeration.Longitude,
                                            Latitude: enumeration.Latitude,
                                            CustomerType: enumeration.CustomerType.ToString(),
                                            Status: enumeration.Status.ToString(),
                                            AccountType: enumeration.AccountType.ToString(),
                                            TariffId: enumeration.TariffId,
                                            ProposedTariffId: enumeration.ProposedTariffId,
                                            IsMetered: enumeration.IsMetered,
                                            Images: enumeration.ImagesUrl);
        });

        // Create the pagination response
        var paginationResponse = new PaginationResponse<EnumerationDetailDto>(
            enumerationDataList,
            enumerationDataList.Count,
            request.PageNumber,
            request.PageSize);

        // Return the API response
        return new ApiResponse<PaginationResponse<EnumerationDetailDto>>(
            true,
            "Enumerations retriened successfully",
            paginationResponse);
    }

    public async Task<ApiResponse<EnumerationDashboardDto>> Dashboard()
    {
        DateTime currentDate = DateTime.Now.Date;
        const string sql = @"
                        DECLARE @TotalCustomersCount INT;
                        DECLARE @EnumeratedCustomersCount INT;

                        -- Calculate Total Customers Count
                        SELECT @TotalCustomersCount = COUNT(DISTINCT c.Id)
                        FROM [Customers] c;

                        -- Calculate Enumerated Customers Count
                        SELECT @EnumeratedCustomersCount = COUNT(DISTINCT en.AccountNumber)
                        FROM [Enumerations] en;

                        SELECT 
                            @TotalCustomersCount AS TotalCustomersCount,
                            @EnumeratedCustomersCount AS EnumeratedCustomersCount,
                            (@TotalCustomersCount - @EnumeratedCustomersCount) AS PendingEnumerationCount,
                            (SELECT COUNT(*) 
                                FROM [Enumerations] en
                                WHERE CONVERT(DATE, en.CreatedOn) = @CurrentDate) AS TodayCount";

        var parameters = new { CurrentDate = currentDate };

        var dashboardData = await _dapperRepository.QueryFirstOrDefaultAsync<EnumerationDashboardDto>(sql, parameters);

        if (dashboardData == null)
        {
            return new ApiResponse<EnumerationDashboardDto>(false, "Enumerations dashboard data not found", default!);
        }

        return new ApiResponse<EnumerationDashboardDto>(true, "Enumerations dashboard data retrieved successfully", dashboardData);
    }

    #endregion
}
