using OpsManagerAPI.Domain.Common;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Domain.Aggregates.CustomerAggregate;

public class Evaluation : AuditableEntity, IAggregateRoot
{
    public DefaultIdType StaffId { get; private set; }
    public string? BuildingStatus { get; private set; }
    public string? ContactSurname { get; private set; }
    public string? ContactFirstname { get; private set; }
    public string? ContactPhone { get; private set; }
    public string? ContactEmail { get; private set; }
    public CustomerType CustomerType { get; private set; }
    public string? State { get; private set; }
    public string? City { get; private set; }
    public string? LGA { get; private set; }
    public string? FeederId { get; private set; }
    public string? DssId { get; private set; }
    public string? CustomerFirstname { get; private set; }
    public string? CustomerLastname { get; private set; }
    public string? CustomerMiddlename { get; private set; }
    public string? CustomerAddress { get; private set; }
    public double CustomerLatitude { get; private set; }
    public double CustomerLongitude { get; private set; }
    public string? CustomerEmail { get; private set; }
    public string? CustomerPhone { get; private set; }
    public CustomerStatus CustomerStatus { get; private set; }
    public string? BusinessNature { get; private set; }
    public string? PremiseUse { get; private set; }
    public bool MeterInstalled { get; private set; }
    public string? AccountNumber { get; private set; }
    public string? MeterNumber { get; private set; }
    public double PresentMeterReadingKwh { get; private set; }
    public double LastActualReadingKwh { get; private set; }
    public double LoadReadingRed { get; private set; }
    public double LoadReadingYellow { get; private set; }
    public double LoadReadingBlue { get; private set; }
    public string? Comment { get; private set; }
    public bool CertifiedOk { get; private set; }
    public double OverdraftKwh { get; private set; }
    public double OverdraftNaira { get; private set; }
    public double TariffDifference { get; private set; }
    public string? MeterImage { get; private set; }
    public string? BypassImage { get; private set; }
    public string? MeterVideo { get; private set; }
    public string? CorrectionImage { get; private set; }
    public string? CorrectionVideo { get; private set; }
    public string? RpaFinalRecommendation { get; private set; }

    public string? ExistingTariffId { get; private set; }
    public string? CorrectTariffId { get; private set; }
    public string? ExistingServiceBand { get; private set; }
    public string? CorrectServiceBand { get; private set; }
    public double Last3MonthsConsumption { get; private set; }
    public double PowerFactor { get; private set; }
    public double LoadFactor { get; private set; }
    public double AvailabilityOfSupply { get; private set; }
    public double EnergyBilledMonth { get; private set; }
    public double EnergyBilledAvg { get; private set; }
    public double LrLyLbAvg { get; private set; }
    public double UnBilledEnergy { get; private set; }
    public int NumOfLorMonth { get; private set; }
    public double LorChargedForMeteringIssues { get; private set; }
    public bool IsSubsequentOffender { get; private set; }
    public double AdministrativeCharge { get; private set; }
    public double LorChargedForEnergyTheft { get; private set; }
    public double PenaltyChargedForEnergyTheft { get; private set; }
    public int NumOfAvailabilityDays { get; private set; }
    public string? Landmark { get; private set; }
    public string? MeterType { get; private set; }
    public string? MeterRating { get; private set; }
    public string? ModeOfPayment { get; private set; }
    public string? MeterMaker { get; private set; }
    public AccountType AccountType { get; private set; }

    #region Constructors
    private Evaluation()
    {
    }

    public Evaluation(
        DefaultIdType staffId,
        string? buildingStatus,
        string? contactSurname,
        string? contactFirstname,
        string? contactPhone,
        string? contactEmail,
        CustomerType customerType,
        string? state,
        string? city,
        string? lGA,
        string? feederId,
        string? dssId,
        string? customerFirstname,
        string? customerLastname,
        string? customerMiddlename,
        string? customerAddress,
        double customerLatitude,
        double customerLongitude,
        string? customerEmail,
        string? customerPhone,
        CustomerStatus customerStatus,
        string? businessNature,
        string? premiseUse,
        bool meterInstalled,
        string? accountNumber,
        string? meterNumber,
        double presentMeterReadingKwh,
        double lastActualReadingKwh,
        double loadReadingRed,
        double loadReadingYellow,
        double loadReadingBlue,
        string? comment,
        bool certifiedOk,
        double overdraftKwh,
        double overdraftNaira,
        double tariffDifference,
        string? meterImage,
        string? bypassImage,
        string? meterVideo,
        string? correctionImage,
        string? correctionVideo,
        string? rpaFinalRecommendation,
        string? existingTariffId,
        string? correctTariffId,
        string? existingServiceBand,
        string? correctServiceBand,
        double last3MonthsConsumption,
        double powerFactor,
        double loadFactor,
        double availabilityOfSupply,
        double energyBilledMonth,
        double energyBilledAvg,
        double lrLyLbAvg,
        double unBilledEnergy,
        int numOfLorMonth,
        double lorChargedForMeteringIssues,
        bool isSubsequentOffender,
        double administrativeCharge,
        double lorChargedForEnergyTheft,
        double penaltyChargedForEnergyTheft,
        int numOfAvailabilityDays,
        string? landmark,
        string? meterType,
        string? meterRating,
        string? modeOfPayment,
        string? meterMaker,
        AccountType accountType)
    {
        StaffId = staffId;
        BuildingStatus = buildingStatus;
        ContactSurname = contactSurname;
        ContactFirstname = contactFirstname;
        ContactPhone = contactPhone;
        ContactEmail = contactEmail;
        CustomerType = customerType;
        State = state;
        City = city;
        LGA = lGA;
        FeederId = feederId;
        DssId = dssId;
        CustomerFirstname = customerFirstname;
        CustomerLastname = customerLastname;
        CustomerMiddlename = customerMiddlename;
        CustomerAddress = customerAddress;
        CustomerLatitude = customerLatitude;
        CustomerLongitude = customerLongitude;
        CustomerEmail = customerEmail;
        CustomerPhone = customerPhone;
        CustomerStatus = customerStatus;
        BusinessNature = businessNature;
        PremiseUse = premiseUse;
        MeterInstalled = meterInstalled;
        AccountNumber = accountNumber;
        MeterNumber = meterNumber;
        PresentMeterReadingKwh = presentMeterReadingKwh;
        LastActualReadingKwh = lastActualReadingKwh;
        LoadReadingRed = loadReadingRed;
        LoadReadingYellow = loadReadingYellow;
        LoadReadingBlue = loadReadingBlue;
        Comment = comment;
        CertifiedOk = certifiedOk;
        OverdraftKwh = overdraftKwh;
        OverdraftNaira = overdraftNaira;
        TariffDifference = tariffDifference;
        MeterImage = meterImage;
        BypassImage = bypassImage;
        MeterVideo = meterVideo;
        CorrectionImage = correctionImage;
        CorrectionVideo = correctionVideo;
        RpaFinalRecommendation = rpaFinalRecommendation;
        ExistingTariffId = existingTariffId;
        CorrectTariffId = correctTariffId;
        ExistingServiceBand = existingServiceBand;
        CorrectServiceBand = correctServiceBand;
        Last3MonthsConsumption = last3MonthsConsumption;
        PowerFactor = powerFactor;
        LoadFactor = loadFactor;
        AvailabilityOfSupply = availabilityOfSupply;
        EnergyBilledMonth = energyBilledMonth;
        EnergyBilledAvg = energyBilledAvg;
        LrLyLbAvg = lrLyLbAvg;
        UnBilledEnergy = unBilledEnergy;
        NumOfLorMonth = numOfLorMonth;
        LorChargedForMeteringIssues = lorChargedForMeteringIssues;
        IsSubsequentOffender = isSubsequentOffender;
        AdministrativeCharge = administrativeCharge;
        LorChargedForEnergyTheft = lorChargedForEnergyTheft;
        PenaltyChargedForEnergyTheft = penaltyChargedForEnergyTheft;
        NumOfAvailabilityDays = numOfAvailabilityDays;
        Landmark = landmark;
        AccountType = accountType;
        MeterType = meterType;
        MeterRating = meterRating;
        ModeOfPayment = modeOfPayment;
        MeterMaker = meterMaker;
    }
    #endregion
}
