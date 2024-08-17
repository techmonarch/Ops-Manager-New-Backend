using OpsManagerAPI.Application.Features.Customers.Dtos;

namespace OpsManagerAPI.Application.Features.Customers.Queries;
public class CustomerQueries : ICustomerQueries
{
    #region Constructor
    private readonly IDapperRepository _dapperRepository;
    private readonly ICustomerRepository _repository;

    public CustomerQueries(IDapperRepository dapperRepository, ICustomerRepository repository)
    {
        _dapperRepository = dapperRepository;
        _repository = repository;
    }

    #endregion

    #region Dapper

    public async Task<ApiResponse<CustomerDetailDto>> GetAsync(string customerId, CancellationToken cancellationToken)
    {
        const string sql = @"
                    SELECT 
                        c.AccountNumber,
                        c.MeterNumber,
                        c.FirstName,
                        c.LastName,
                        c.Phone,
                        c.City,
                        c.State,
                        t.ServiceBandName AS ServiceBand,
                        CASE c.CustomerType
                            WHEN 1 THEN 'MD'
                            WHEN 2 THEN 'NMD'
                            ELSE ''
                        END AS CustomerType,
                        CASE c.AccountType
                            WHEN 1 THEN 'Prepaid'
                            WHEN 2 THEN 'Postpaid'
                            ELSE ''
                        END AS AccountType,
                        CASE c.Status
                            WHEN 1 THEN  'Active'
                            WHEN 2 THEN 'NonActive'
                            ELSE 'NIL'
                        END AS Status
                    FROM 
                        Customers c
                   INNER JOIN 
                        OpsManager.Tariffs t ON c.TariffId = t.Id
                    WHERE 
                        c.Id = @CustomerId";

        var parameters = new { CustomerId = customerId };

        var customer = await _dapperRepository.QueryFirstOrDefaultAsync<CustomerDetailDto>(sql, parameters, cancellationToken: cancellationToken);

        if (customer == null)
        {
            return new ApiResponse<CustomerDetailDto>(false, "Customer not found", default!);
        }

        return new ApiResponse<CustomerDetailDto>(true, "Customer retrieved successfully", customer);
    }

    public async Task<ApiResponse<CustomerDetailDto>> GetByAccountNumberAsync(string accountNumber, CancellationToken cancellationToken)
    {
        const string sql = @"
                    SELECT 
                        c.AccountNumber,
                        c.MeterNumber,
                        c.FirstName,
                        c.LastName,
                        c.Phone,
                        c.City,
                        c.State,
                        t.ServiceBandName AS ServiceBand,
                        CASE c.CustomerType
                            WHEN 1 THEN 'MD'
                            WHEN 2 THEN 'NMD'
                            ELSE ''
                        END AS CustomerType,
                        CASE c.AccountType
                            WHEN 1 THEN 'Prepaid'
                            WHEN 2 THEN 'Postpaid'
                            ELSE ''
                        END AS AccountType,
                        CASE c.Status
                            WHEN 1 THEN  'Active'
                            WHEN 2 THEN 'NonActive'
                            ELSE 'NIL'
                        END AS Status
                    FROM 
                        Customers c
                   INNER JOIN 
                        OpsManager.Tariffs t ON c.TariffId = t.Id
                    WHERE 
                        c.AccountNumber = @AccountNumber";

        var parameters = new { AccountNumber = accountNumber };

        var customer = await _dapperRepository.QueryFirstOrDefaultAsync<CustomerDetailDto>(sql, parameters, cancellationToken: cancellationToken);

        if (customer == null)
        {
            return new ApiResponse<CustomerDetailDto>(false, "Customer not found", default!);
        }

        return new ApiResponse<CustomerDetailDto>(true, "Customer retrieved successfully", customer);
    }

    #endregion

    #region EF-Core
    public async Task<ApiResponse<PaginationResponse<CustomerDataDto>>> SearchAsync(CustomerFilterRequest request, CancellationToken cancellationToken)
    {
        var (customers, totalCount) = await _repository.SearchAsync(request, cancellationToken);

        // Map customers to CustomerDataDto
        var customerDataList = customers.ConvertAll(customer =>
        {
            return new CustomerDataDto(
                Id: customer.Id.ToString(),
                AccountNumber: customer.AccountNumber,
                MeterNumber: customer.MeterNumber,
                Phone: customer.Phone,
                City: customer.City,
                State: customer.State,
                ServiceBand: customer?.Tariff?.ServiceBandName!,
                CustomerType: customer.CustomerType.ToString(),
                AccountType: customer.AccountType.ToString(),
                Status: customer.Status.ToString(),
                FirstName: customer.FirstName,
                MiddleName: customer.MiddleName,
                LastName: customer.LastName,
                Address: customer.Address,
                Email: customer.Email,
                DistributionTransformerId: customer.DistributionTransformer.Id.ToString());
        });

        // Create the pagination response
        var paginationResponse = new PaginationResponse<CustomerDataDto>(
            customerDataList,
            totalCount,
            request.PageNumber,
            request.PageSize);

        // Return the API response
        return new ApiResponse<PaginationResponse<CustomerDataDto>>(true, "Customers retrieved successfully", paginationResponse);
    }

    public async Task<ApiResponse<PaginationResponse<EvaluationsAndEnumerationsCustomerDataDto>>> GetForEvaluationsAndEnumerations(CustomerFilterRequest request, CancellationToken cancellationToken)
    {
        var (customers, totalCount) = await _repository.GetForEvaluationsAndEnumerations(request, cancellationToken);

        // Map customers to CustomerDataDto
        var customerDataList = customers.ConvertAll(customer =>
        {
            bool isEvaluated = customer.Evaluations.Any();
            bool isEnumerated = customer.Enumerations.Any();

            return new EvaluationsAndEnumerationsCustomerDataDto(
                Id: customer.Id.ToString(),
                AccountNumber: customer.AccountNumber,
                MeterNumber: customer.MeterNumber,
                Phone: customer.Phone,
                City: customer.City,
                State: customer.State,
                ServiceBand: customer?.Tariff?.ServiceBandName!,
                CustomerType: customer.CustomerType.ToString(),
                AccountType: customer.AccountType.ToString(),
                Status: customer.Status.ToString(),
                FirstName: customer.FirstName,
                MiddleName: customer.MiddleName,
                LastName: customer.LastName,
                Address: customer.Address,
                Email: customer.Email,
                DistributionTransformerId: customer.DistributionTransformer.Id.ToString(),
                IsCustomerEvaluated: isEvaluated,
                IsCustomerEnumerated: isEnumerated);
        });

        // Create the pagination response
        var paginationResponse = new PaginationResponse<EvaluationsAndEnumerationsCustomerDataDto>(
            customerDataList,
            totalCount, // Use the correct total count here
            request.PageNumber,
            request.PageSize);

        // Return the API response
        return new ApiResponse<PaginationResponse<EvaluationsAndEnumerationsCustomerDataDto>>(true, "Customers retrieved successfully", paginationResponse);
    }

    public async Task<ApiResponse<PaginationResponse<MeterReadingsAndBillDistributionsCustomerDataDto>>> GetForMeterReadingsAndBillDistributions(CustomerFilterRequest request, CancellationToken cancellationToken)
    {
        var (customers, totalCount) = await _repository.GetForMeterReadingsAndBillDistributions(request, cancellationToken);

        // Map customers to CustomerDataDto
        var customerDataList = customers.ConvertAll(customer =>
        {
            var mostRecentReading = customer.MeterReadings
                                            .OrderByDescending(mr => mr.LastModifiedOn)
                                            .FirstOrDefault();

            var lastReadingDate = mostRecentReading?.LastModifiedOn ?? DateTime.MinValue;
            decimal lastReading = mostRecentReading?.PreviousReading ?? 0m;
            bool isMeterReadingCaptured = customer.MeterReadings
                                                  .Any(mr => mr.LastModifiedOn?.Month == DateTime.Now.Month && mr.LastModifiedOn?.Year == DateTime.Now.Year);

            bool isBillDistributed = customer.BillDistributions
                                             .Any(b => b.DistributionDate.HasValue &&
                                                       b.DistributionDate.Value.Month == DateTime.Now.Month &&
                                                       b.DistributionDate.Value.Year == DateTime.Now.Year);

            return new MeterReadingsAndBillDistributionsCustomerDataDto(
                Id: customer.Id.ToString(),
                AccountNumber: customer.AccountNumber,
                MeterNumber: customer.MeterNumber,
                Phone: customer.Phone,
                City: customer.City,
                State: customer.State,
                ServiceBand: customer?.Tariff?.ServiceBandName!,
                CustomerType: customer.CustomerType.ToString(),
                AccountType: customer.AccountType.ToString(),
                Status: customer.Status.ToString(),
                FirstName: customer.FirstName,
                MiddleName: customer.MiddleName,
                LastName: customer.LastName,
                Address: customer.Address,
                LastReadingDate: lastReadingDate,
                LastReading: lastReading,
                IsMeterReadingCaptured: isMeterReadingCaptured,
                Email: customer.Email,
                IsBillDistributed: isBillDistributed,
                DistributionTransformerId: customer.DistributionTransformer.Id.ToString());
        });

        // Create the pagination response
        var paginationResponse = new PaginationResponse<MeterReadingsAndBillDistributionsCustomerDataDto>(
            customerDataList,
            totalCount, // Use the correct total count here
            request.PageNumber,
            request.PageSize);

        // Return the API response
        return new ApiResponse<PaginationResponse<MeterReadingsAndBillDistributionsCustomerDataDto>>(true, "Customers retrieved successfully", paginationResponse);
    }
    #endregion
}
