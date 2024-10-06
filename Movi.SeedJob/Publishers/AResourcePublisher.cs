using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.SeedJob.Publishers;

public interface IResourcePublisher
{
    int Order { get; }
    Task<object> PublishAsync();
}

public abstract class AResourcePublisher<T>(IBulkRepository context, UserManager<ApplicationUser> manager)
    : IResourcePublisher where T : class, IDatabaseModel
{
    public abstract int Order { get; }
    public abstract string FileName { get; }
    public virtual bool Cleanup { get; } = true;
    public virtual bool IsArray { get; } = true;

    protected readonly IBulkRepository _context = context;
    protected readonly UserManager<ApplicationUser> _manager = manager;

    public Type GetModelType()
        => IsArray ? typeof(List<T>) : typeof(T);

    public virtual object Deserialize()
        => JsonSerializer.Deserialize(File.ReadAllText(FileName), GetModelType(), new JsonSerializerOptions()
        {
            MaxDepth = 10,
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true
        });

    public virtual void ApplyFilter(params T[] entities) { }

    public virtual async Task<object> PublishAsync()
    {
        if (Cleanup) await CleanupDatabaseAsync();

        var obj = Deserialize();

        if (obj is List<T> models)
        {
            ApplyFilter([.. models]);
            await _context.AddAsync(models);
            await _context.UpdateAsync(models);
        }
        else if (obj is T model)
        {
            ApplyFilter(model);
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
