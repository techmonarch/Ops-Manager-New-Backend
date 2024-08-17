using Microsoft.AspNetCore.Http;
using OpsManagerAPI.Application.Features.Enumerations.Specifications;
using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.Enumerations.Commands;

public class CreateEnumerationCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public string MeterNumber { get; set; } = default!;
    public string AccountNumber { get; set; } = default!;
    public DefaultIdType? DistributionTransformerId { get; set; }
    public string FirstName { get; set; } = default!;
    public string ContactFirstName { get; set; } = default!;
    public string MiddleName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string ContactLastName { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string ContactPhone { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? ContactEmail { get; set; }
    public string City { get; set; } = default!;
    public string LGA { get; set; } = default!;
    public string State { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string BuildingDescription { get; set; } = default!;
    public string Landmark { get; set; } = default!;
    public string BusinessType { get; set; } = default!;
    public string PremiseType { get; set; } = default!;
    public string ServiceBand { get; set; } = default!;
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
    public CustomerType CustomerType { get; set; }
    public CustomerStatus Status { get; set; }
    public AccountType AccountType { get; set; }
    public DefaultIdType? TariffId { get; set; }
    public DefaultIdType? ProposedTariffId { get; set; }
    public bool IsMetered { get; set; }
    public List<IFormFile> Images { get; set; } = new();
}

public class CreateEnumerationCommandHandler : IRequestHandler<CreateEnumerationCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepositoryWithEvents<Enumeration> _repository;
    private readonly IStaffRepository _staffRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IFileStorageService _fileStorage;

    public CreateEnumerationCommandHandler(IRepositoryWithEvents<Enumeration> repository, ICurrentUser currentUser, IStaffRepository staffRepository, IFileStorageService fileStorage)
        => (_repository, _currentUser, _staffRepository, _fileStorage) = (repository, currentUser, staffRepository, fileStorage);

    public async Task<ApiResponse<DefaultIdType>> Handle(CreateEnumerationCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser.GetUserId();
        var currentStaff = await _staffRepository.GetByUserIdAsync(currentUserId, cancellationToken);

        // Check if an enumeration already exists for the given account number
        var existingEnumeration = await _repository.GetBySpecAsync(new EnumerationByAccountNumberSpec(request.AccountNumber), cancellationToken);

        if (existingEnumeration != null)
        {
            var imagesUrl = await _fileStorage.UploadMultipleAsync(request.Images, FileType.Image, cancellationToken);

            // Update the existing enumeration
            existingEnumeration.UpdateMeterNumber(request.MeterNumber);
            existingEnumeration.UpdateAccountNumber(request.AccountNumber);
            existingEnumeration.UpdateDistributionTransformer(request.DistributionTransformerId);
            existingEnumeration.UpdatePersonalInfo(request.FirstName, request.MiddleName, request.LastName, request.Gender);
            existingEnumeration.UpdateContactInfo(request.ContactFirstName, request.ContactLastName, request.Phone, request.ContactPhone, request.Email, request.ContactEmail);
            existingEnumeration.UpdateAddress(request.City, request.LGA, request.State, request.Address, request.BuildingDescription, request.Landmark);
            existingEnumeration.UpdateBusinessDetails(request.BusinessType, request.PremiseType, request.ServiceBand);
            existingEnumeration.UpdateCoordinates(request.Longitude, request.Latitude);
            existingEnumeration.UpdateCustomerType(request.CustomerType);
            existingEnumeration.UpdateStatus(request.Status);
            existingEnumeration.UpdateAccountType(request.AccountType);
            existingEnumeration.UpdateTariff(request.TariffId);
            existingEnumeration.UpdateProposedTariff(request.ProposedTariffId);
            existingEnumeration.UpdateStaffId(currentStaff.Id);
            existingEnumeration.UpdateMeteredStatus(request.IsMetered);
            existingEnumeration.UpdateImagesUrl(imagesUrl);

            await _repository.UpdateAsync(existingEnumeration, cancellationToken);
            return new ApiResponse<DefaultIdType>(true, "Customer enumeration updated successfully", existingEnumeration.Id);
        }
        else
        {
            var imagesUrl = await _fileStorage.UploadMultipleAsync(request.Images, FileType.Image, cancellationToken);

            // Create a new enumeration
            var enumeration = new Enumeration(
                meterNumber: request.MeterNumber,
                accountNumber: request.AccountNumber,
                distributionTransformerId: request.DistributionTransformerId,
                firstName: request.FirstName,
                contactFirstName: request.ContactFirstName,
                middleName: request.MiddleName,
                lastName: request.LastName,
                contactLastName: request.ContactLastName,
                gender: request.Gender,
                phone: request.Phone,
                contactPhone: request.ContactPhone,
                email: request.Email,
                contactEmail: request.ContactEmail,
                city: request.City,
                lGA: request.LGA,
                state: request.State,
                address: request.Address,
                buildingDescription: request.BuildingDescription,
                landmark: request.Landmark,
                businessType: request.BusinessType,
                premiseType: request.PremiseType,
                serviceBand: request.ServiceBand,
                longitude: request.Longitude,
                latitude: request.Latitude,
                customerType: request.CustomerType,
                status: request.Status,
                accountType: request.AccountType,
                tariffId: request.TariffId,
                proposedTariffId: request.ProposedTariffId,
                staffId: currentStaff.Id,
                isMetered: request.IsMetered,
                imagesUrl: imagesUrl);

            await _repository.AddAsync(enumeration, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return new ApiResponse<DefaultIdType>(true, "Customer successfully enumerated", enumeration.Id);
        }
    }
}
