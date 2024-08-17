using System.Collections.ObjectModel;

namespace OpsManagerAPI.Infrastructure.Authorization;
public static class OPSAction
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string UpgradeSubscription = nameof(UpgradeSubscription);
}

public static class OPSResource
{
    public const string Tenants = nameof(Tenants);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Tariffs = nameof(Tariffs);
    public const string DistributionTransformers = nameof(DistributionTransformers);
    public const string BillDistributions = nameof(BillDistributions);
    public const string MeterReadings = nameof(MeterReadings);
    public const string Evaluations = nameof(Evaluations);
    public const string Enumerations = nameof(Enumerations);
    public const string Customers = nameof(Customers);
    public const string Disconnections = nameof(Disconnections);
    public const string DisconnectionReasons = nameof(DisconnectionReasons);
    public const string Reconnections = nameof(Reconnections);
    public const string Complaints = nameof(Complaints);
    public const string Payments = nameof(Payments);
    public const string Billings = nameof(Billings);
    public const string ComplaintCategories = nameof(ComplaintCategories);
    public const string ComplaintSubCategories = nameof(ComplaintSubCategories);
    public const string Teams = nameof(Teams);
    public const string DownloadManagers = nameof(DownloadManagers);
    public const string WebDashboards = nameof(WebDashboards);
}

public static class OPSPermissions
{
    private static readonly OPSPermission[] _all = new OPSPermission[]
    {
        new("View Users", OPSAction.View, OPSResource.Users, IsBasic: true),
        new("Search Users", OPSAction.Search, OPSResource.Users, IsBasic: true),
        new("Create Users", OPSAction.Create, OPSResource.Users),
        new("Update Users", OPSAction.Update, OPSResource.Users),
        new("Delete Users", OPSAction.Delete, OPSResource.Users),
        new("Export Users", OPSAction.Export, OPSResource.Users),

        new("View UserRoles", OPSAction.View, OPSResource.UserRoles),
        new("Update UserRoles", OPSAction.Update, OPSResource.UserRoles),

        new("View Roles", OPSAction.View, OPSResource.Roles),
        new("Create Roles", OPSAction.Create, OPSResource.Roles),
        new("Update Roles", OPSAction.Update, OPSResource.Roles),
        new("Delete Roles", OPSAction.Delete, OPSResource.Roles),

        new("View RoleClaims", OPSAction.View, OPSResource.RoleClaims),
        new("Update RoleClaims", OPSAction.Update, OPSResource.RoleClaims),

        new("View Tenants", OPSAction.View, OPSResource.Tenants, IsRoot: true),
        new("Create Tenants", OPSAction.Create, OPSResource.Tenants, IsRoot: true),
        new("Update Tenants", OPSAction.Update, OPSResource.Tenants, IsRoot: true),
        new("Upgrade Tenant Subscription", OPSAction.UpgradeSubscription, OPSResource.Tenants, IsRoot: true),

        new("Create Bill Distribution", OPSAction.Create, OPSResource.BillDistributions, IsBasic: true),
        new("View Bill Distribution", OPSAction.View, OPSResource.BillDistributions, IsBasic: true),

        new("Create Meter Readings", OPSAction.Create, OPSResource.MeterReadings, IsBasic: true),
        new("Update Meter Readings", OPSAction.Update, OPSResource.MeterReadings, IsBasic: true),
        new("View Meter Readings", OPSAction.View, OPSResource.MeterReadings, IsBasic: true),

        new("Create Reconnections", OPSAction.Create, OPSResource.Reconnections, IsBasic: true),
        new("Update Reconnections", OPSAction.Update, OPSResource.Reconnections, IsBasic: true),
        new("View Reconnections", OPSAction.View, OPSResource.Reconnections, IsBasic: true),

        new("Create DisconnectionReasons", OPSAction.Create, OPSResource.DisconnectionReasons, IsBasic: true),

        new("Create Disconnections", OPSAction.Create, OPSResource.Disconnections, IsBasic: true),
        new("Update Disconnections", OPSAction.Update, OPSResource.Disconnections, IsBasic: true),
        new("View Disconnections", OPSAction.View, OPSResource.Disconnections, IsBasic: true),

        new("Create Complaints", OPSAction.Create, OPSResource.Complaints, IsBasic: true),
        new("Update Complaints", OPSAction.Update, OPSResource.Complaints, IsBasic: true),
        new("View Complaints", OPSAction.View, OPSResource.Complaints, IsBasic: true),

        new("Create Billings", OPSAction.Create, OPSResource.Billings, IsBasic: true),
        new("Update Billings", OPSAction.Update, OPSResource.Billings, IsBasic: true),
        new("View Billings", OPSAction.View, OPSResource.Billings, IsBasic: true),

        new("Create Payments", OPSAction.Create, OPSResource.Payments, IsBasic: true),
        new("Update Payments", OPSAction.Update, OPSResource.Payments, IsBasic: true),
        new("View Payments", OPSAction.View, OPSResource.Payments, IsBasic: true),

        new("Create Download Managers", OPSAction.Create, OPSResource.DownloadManagers, IsBasic: true),
        new("Update Download Managers", OPSAction.Update, OPSResource.DownloadManagers, IsBasic: true),
        new("View Download Managers", OPSAction.View, OPSResource.DownloadManagers, IsBasic: true),
        new("Delete Download Managers", OPSAction.Delete, OPSResource.DownloadManagers, IsBasic: true),

        new("Create Complaint Categories", OPSAction.Create, OPSResource.ComplaintCategories, IsBasic: true),
        new("Update Complaint Categories", OPSAction.Update, OPSResource.ComplaintCategories, IsBasic: true),
        new("View Complaint Categories", OPSAction.View, OPSResource.ComplaintCategories, IsBasic: true),

        new("Create Complaint Sub-Categories", OPSAction.Create, OPSResource.ComplaintSubCategories, IsBasic: true),
        new("Update Complaint Sub-Categories", OPSAction.Update, OPSResource.ComplaintSubCategories, IsBasic: true),
        new("View Complaint Sub-Categories", OPSAction.View, OPSResource.ComplaintSubCategories, IsBasic: true),

        new("Create Teams", OPSAction.Create, OPSResource.Teams, IsBasic: true),
        new("Update Teams", OPSAction.Update, OPSResource.Teams, IsBasic: true),
        new("View Teams", OPSAction.View, OPSResource.Teams, IsBasic: true),

        new("Create Evaluation", OPSAction.Create, OPSResource.Evaluations, IsBasic: true),
        new("View Evaluation", OPSAction.View, OPSResource.Evaluations, IsBasic: true),

        new("Create Enumeration", OPSAction.Create, OPSResource.Enumerations, IsBasic: true),
        new("View Enumeration", OPSAction.View, OPSResource.Enumerations, IsBasic: true),

        new("Search Customer", OPSAction.Search, OPSResource.Customers, IsBasic: true),
        new("View Customers", OPSAction.View, OPSResource.Customers, IsBasic: true),

        new("View Web Dashboards", OPSAction.View, OPSResource.WebDashboards, IsBasic: true),

        new("View Tariffs", OPSAction.View, OPSResource.Tariffs, IsBasic: true),

        new("View Distribution Transformers", OPSAction.View, OPSResource.DistributionTransformers, IsBasic: true),
        new("Search Distribution Transformers", OPSAction.Search, OPSResource.DistributionTransformers, IsBasic: true),
    };

    public static IReadOnlyList<OPSPermission> All { get; } = new ReadOnlyCollection<OPSPermission>(_all);
    public static IReadOnlyList<OPSPermission> Root { get; } = new ReadOnlyCollection<OPSPermission>(_all.Where(p => p.IsRoot).ToArray());
    public static IReadOnlyList<OPSPermission> Admin { get; } = new ReadOnlyCollection<OPSPermission>(_all.Where(p => !p.IsRoot).ToArray());
    public static IReadOnlyList<OPSPermission> CRO { get; } = new ReadOnlyCollection<OPSPermission>(_all.Where(p => p.IsBasic).ToArray());
    public static IReadOnlyList<OPSPermission> BHTE { get; } = new ReadOnlyCollection<OPSPermission>(_all.Where(p => p.IsBasic).ToArray());
    public static IReadOnlyList<OPSPermission> CLW { get; } = new ReadOnlyCollection<OPSPermission>(_all.Where(p => p.IsBasic).ToArray());
}

public record OPSPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}
