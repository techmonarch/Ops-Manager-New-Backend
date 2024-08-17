using Finbuckle.MultiTenant;
using Mapster;
using Microsoft.Extensions.Options;
using OpsManagerAPI.Application.Common.Exceptions;
using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Common.Persistence;
using OpsManagerAPI.Application.Features.Multitenancy.Commands;
using OpsManagerAPI.Application.Features.Multitenancy.Contracts;
using OpsManagerAPI.Application.Features.Multitenancy.Dtos;
using OpsManagerAPI.Infrastructure.Persistence;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;

namespace OpsManagerAPI.Infrastructure.Multitenancy;
internal class DiscoService : IDiscoService
{
    private readonly IMultiTenantStore<DiscoInfo> _discoStore;
    private readonly IConnectionStringSecurer _csSecurer;
    private readonly IDatabaseInitializer _dbInitializer;
    private readonly DatabaseSettings _dbSettings;

    public DiscoService(
        IMultiTenantStore<DiscoInfo> DiscoStore,
        IConnectionStringSecurer csSecurer,
        IDatabaseInitializer dbInitializer,
        IOptions<DatabaseSettings> dbSettings)
    {
        _discoStore = DiscoStore;
        _csSecurer = csSecurer;
        _dbInitializer = dbInitializer;
        _dbSettings = dbSettings.Value;
    }

    public async Task<ApiResponse<List<DiscoDto>>> GetAllAsync()
    {
        var discos = (await _discoStore.GetAllAsync()).Adapt<List<DiscoDto>>();
        discos.ForEach(t => t.ConnectionString = _csSecurer.MakeSecure(t.ConnectionString));
        return new ApiResponse<List<DiscoDto>>(true, "Discos retrieved successfully", discos);
    }

    public async Task<bool> ExistsWithIdAsync(string id) =>
        await _discoStore.TryGetAsync(id) is not null;

    public async Task<bool> ExistsWithNameAsync(string name) =>
        (await _discoStore.GetAllAsync()).Any(t => t.Name == name);

    public async Task<ApiResponse<DiscoDto>> GetByIdAsync(string id) =>
        new ApiResponse<DiscoDto>(true, "Disco retrieved successfully", (await GetDiscoInfoAsync(id))
            .Adapt<DiscoDto>());

    public async Task<ApiResponse<string>> CreateAsync(CreateDiscoRequest request, CancellationToken cancellationToken)
    {
        if (request.ConnectionString?.Trim() == _dbSettings.ConnectionString.Trim()) request.ConnectionString = string.Empty;

        var disco = new DiscoInfo(request.Id, request.Name, request.ConnectionString, request.AdminEmail, request.Issuer);
        await _discoStore.TryAddAsync(disco);

        // TODO: run this in a hangfire job? will then have to send mail when it's ready or not
        try
        {
            await _dbInitializer.InitializeApplicationDbForDiscoAsync(disco, cancellationToken);
        }
        catch
        {
            await _discoStore.TryRemoveAsync(request.Id);
            throw;
        }

        return new ApiResponse<string>(true, "Discos created successfully", disco.Id);
    }

    public async Task<ApiResponse<string>> ActivateAsync(string id)
    {
        var disco = await GetDiscoInfoAsync(id);

        if (disco.IsActive)
        {
            throw new ConflictException("Disco is already Activated.");
        }

        disco.Activate();

        await _discoStore.TryUpdateAsync(disco);

        return new ApiResponse<string>(true, string.Format("Disco {0} is now Activated.", id), string.Format("Disco {0} is now Activated.", id));
    }

    public async Task<ApiResponse<string>> DeactivateAsync(string id)
    {
        var disco = await GetDiscoInfoAsync(id);
        if (!disco.IsActive)
        {
            throw new ConflictException("Disco is already Deactivated.");
        }

        disco.Deactivate();
        await _discoStore.TryUpdateAsync(disco);
        return new ApiResponse<string>(true, string.Format("Disco {0} is now Deactivated.", id), string.Format("Disco {0} is now Deactivated.", id));
    }

    public async Task<ApiResponse<string>> UpdateSubscription(string id, DateTime extendedExpiryDate)
    {
        var disco = await GetDiscoInfoAsync(id);
        disco.SetValidity(extendedExpiryDate);
        await _discoStore.TryUpdateAsync(disco);
        return new ApiResponse<string>(true, string.Format("Disco {0}'s Subscription Upgraded. Now Valid till {1}.", id, disco.ValidUpto), string.Format("Disco {0}'s Subscription Upgraded. Now Valid till {1}.", id, disco.ValidUpto));
    }

    private async Task<DiscoInfo> GetDiscoInfoAsync(string id) =>
        await _discoStore.TryGetAsync(id)
            ?? throw new NotFoundException(string.Format("{0} {1} Not Found.", typeof(DiscoInfo).Name, id));
}