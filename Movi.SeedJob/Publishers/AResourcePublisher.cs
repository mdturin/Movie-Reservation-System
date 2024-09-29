using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.SeedJob.Publishers;

public interface IResourcePublisher
{
    Task<object> PublishAsync();
}

public abstract class AResourcePublisher<T>(IBulkRepository context, UserManager<ApplicationUser> manager)
    : IResourcePublisher where T : class, IDatabaseModel
{
    public abstract string FileName { get; }
    public virtual bool Cleanup { get; } = true;
    public virtual bool IsArray { get; } = false;

    protected readonly IBulkRepository _context = context;
    protected readonly UserManager<ApplicationUser> _manager = manager;

    public Type GetModelType()
        => IsArray ? typeof(List<T>) : typeof(T);

    public virtual object Deserialize()
        => JsonSerializer.Deserialize(File.ReadAllText(FileName), GetModelType());

    public virtual async Task<object> PublishAsync()
    {
        if (Cleanup) await CleanupDatabaseAsync();

        var obj = Deserialize();

        if (obj is List<T> models)
        {
            await _context.AddAsync(models);
        }
        else if (obj is T model)
        {
            await _context.AddAsync(model);
        }

        return obj;
    }

    public virtual async Task CleanupDatabaseAsync()
    {
        var models = await _context.GetAllAsync<T>();
        await _context
            .DeleteAsync<T>(models.Select(m => m.Id));
    }
}
