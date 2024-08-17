using OpsManagerAPI.Domain.Aggregates.MeterAggregate;
using OpsManagerAPI.Domain.Aggregates.StaffAggregate;
using OpsManagerAPI.Domain.Aggregates.TariffsAggregate;
using OpsManagerAPI.Domain.Common;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Domain.Aggregates.CustomerAggregate;

public class Enumeration : AuditableEntity, IAggregateRoot
{
    public string? MeterNumber { get; private set; }
    public string AccountNumber { get; private set; } = default!;
    public DefaultIdType? CustomerId { get; private set; }
    public DefaultIdType? DistributionTransformerId { get; private set; }
    public string FirstName { get; private set; } = default!;
    public string ContactFirstName { get; private set; } = default!;
    public string MiddleName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string ContactLastName { get; private set; } = default!;
    public string Gender { get; private set; } = default!;
    public string Phone { get; private set; } = default!;
    public string ContactPhone { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string? ContactEmail { get; private set; }
    public string City { get; private set; } = default!;
    public string LGA { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string BuildingDescription { get; private set; } = default!;
    public string Landmark { get; private set; } = default!;
    public string BusinessType { get; private set; } = default!;
    public string PremiseType { get; private set; } = default!;
    public string ServiceBand { get; private set; } = default!;
    public decimal Longitude { get; private set; } = default!;
    public decimal Latitude { get; private set; } = default!;
    public CustomerType CustomerType { get; private set; }
    public CustomerStatus Status { get; private set; }
    public AccountType AccountType { get; private set; }
    public DefaultIdType? TariffId { get; private set; }
    public DefaultIdType? ProposedTariffId { get; private set; }
    public DefaultIdType StaffId { get; private set; }
    public bool IsMetered { get; private set; }
    public List<string> ImagesUrl { get; private set; } = new();

    #region Relationships
    public Tariff? Tariff { get; private set; }
    public Staff? Staff { get; private set; }
    public DistributionTransformer? DistributionTransformer { get; private set; }
    #endregion

    #region Constructors
    private Enumeration()
    {
    }

    public Enumeration(
        string meterNumber,
        string accountNumber,
        DefaultIdType? distributionTransformerId,
        string firstName,
        string contactFirstName,
        string middleName,
        string lastName,
        string contactLastName,
        string gender,
        string phone,
        string contactPhone,
        string email,
        string? contactEmail,
        string city,
        string lGA,
        string state,
        string address,
        string buildingDescription,
        string landmark,
        string businessType,
        string premiseType,
        string serviceBand,
        decimal longitude,
        decimal latitude,
        CustomerType customerType,
        CustomerStatus status,
        AccountType accountType,
        DefaultIdType? tariffId,
        DefaultIdType? proposedTariffId,
        DefaultIdType staffId,
        bool isMetered,
        List<string> imagesUrl)
    {
        MeterNumber = meterNumber;
        AccountNumber = accountNumber;
        DistributionTransformerId = distributionTransformerId;
        FirstName = firstName;
        ContactFirstName = contactFirstName;
        MiddleName = middleName;
        LastName = lastName;
        ContactLastName = contactLastName;
        Gender = gender;
        Phone = phone;
        ContactPhone = contactPhone;
        Email = email;
        ContactEmail = contactEmail;
        City = city;
        LGA = lGA;
        State = state;
        Address = address;
        BuildingDescription = buildingDescription;
        Landmark = landmark;
        BusinessType = businessType;
        PremiseType = premiseType;
        ServiceBand = serviceBand;
        Longitude = longitude;
        Latitude = latitude;
        CustomerType = customerType;
        Status = status;
        AccountType = accountType;
        TariffId = tariffId;
        ProposedTariffId = proposedTariffId;
        StaffId = staffId;
        IsMetered = isMetered;
        ImagesUrl = imagesUrl ?? new List<string>();
    }
    #endregion

    #region Behaviors
    public void UpdateMeterNumber(string meterNumber)
    {
        MeterNumber = meterNumber;
    }

    public void UpdateAccountNumber(string accountNumber)
    {
        AccountNumber = accountNumber;
    }

    public void UpdateDistributionTransformer(DefaultIdType? distributionTransformerId)
    {
        DistributionTransformerId = distributionTransformerId;
    }

    public void UpdatePersonalInfo(string firstName, string middleName, string lastName, string gender)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Gender = gender;
    }

    public void UpdateContactInfo(string contactFirstName, string contactLastName, string phone, string contactPhone, string email, string? contactEmail)
    {
        ContactFirstName = contactFirstName;
        ContactLastName = contactLastName;
        Phone = phone;
        ContactPhone = contactPhone;
        Email = email;
        ContactEmail = contactEmail;
    }

    public void UpdateAddress(string city, string lGA, string state, string address, string buildingDescription, string landmark)
    {
        City = city;
        LGA = lGA;
        State = state;
        Address = address;
        BuildingDescription = buildingDescription;
        Landmark = landmark;
    }

    public void UpdateBusinessDetails(string businessType, string premiseType, string serviceBand)
    {
        BusinessType = businessType;
        PremiseType = premiseType;
        ServiceBand = serviceBand;
    }

    public void UpdateCoordinates(decimal longitude, decimal latitude)
    {
        Longitude = longitude;
        Latitude = latitude;
    }

    public void UpdateCustomerType(CustomerType customerType)
    {
        CustomerType = customerType;
    }

    public void UpdateStatus(CustomerStatus status)
    {
        Status = status;
    }

    public void UpdateAccountType(AccountType accountType)
    {
        AccountType = accountType;
    }

    public void UpdateTariff(DefaultIdType? tariffId)
    {
        TariffId = tariffId;
    }

    public void UpdateProposedTariff(DefaultIdType? proposedTariffId)
    {
        ProposedTariffId = proposedTariffId;
    }

    public void UpdateStaffId(DefaultIdType staffId)
    {
        StaffId = staffId;
    }

    public void UpdateMeteredStatus(bool isMetered)
    {
        IsMetered = isMetered;
    }

    public void UpdateImagesUrl(List<string> imagesUrl)
    {
        ImagesUrl = imagesUrl;
    }
    #endregion
}
