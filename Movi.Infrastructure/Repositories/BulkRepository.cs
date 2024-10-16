using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Movi.Core.Domain.Interfaces;
using Movi.Infrastructure.Data;
using Movi.Infrastructure.Extensions;

namespace Movi.Infrastructure.Repositories;

public class BulkRepository(ApplicationDbContext context) : IBulkRepository
{
    private readonly ApplicationDbContext _context = context;
    protected DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class, IDatabaseModel
        => _context.Set<TEntity>();

    protected IQueryable<TEntity> GetDbSetAsNoTrackingQueryable<TEntity>()
        where TEntity : class, IDatabaseModel
            => _context.Set<TEntity>().AsNoTracking().AsQueryable();

    public IDbContextTransaction BeginTransaction()
        => _context.Database.BeginTransaction();

    public Task<int> SaveChangesAsync()
        => _context.SaveChangesAsync();

    public async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class, IDatabaseModel
    {
        await GetDbSet<TEntity>().AddAsync(entity);
    }

    public async Task DeleteAsync<TEntity>(string id)
        where TEntity : class, IDatabaseModel
    {
        var entity = await GetDbSet<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

        if (entity != null)
        {
            GetDbSet<TEntity>().Remove(entity);
        }
    }

    public async Task DeleteAsync<TEntity>(IEnumerable<string> ids)
        where TEntity : class, IDatabaseModel
    {
        await GetDbSet<TEntity>()
            .AsNoTracking()
            .Where(entity => ids.Contains(entity.Id))
            .ExecuteDeleteAsync();
    }

    public Task<TEntity> GetItemAsync<TEntity>(
        Expression<Func<TEntity, bool>> conditionExpression,
        params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IDatabaseModel
    {
        return GetDbSetAsNoTrackingQueryable<TEntity>()
            .ApplyIncludes(includes)
            .FirstOrDefaultAsync(conditionExpression);
    }

    public Task<List<TEntity>> GetItemsAsync<TEntity>(
        Expression<Func<TEntity, bool>> conditionExpression,
        params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IDatabaseModel
    {
        try
        {
            var query = GetDbSetAsNoTrackingQueryable<TEntity>();

            // Apply includes if any are provided
            if (includes != null && includes.Length != 0)
            {
                query = query.ApplyIncludes(includes);
            }

            return query
                .Where(conditionExpression)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            // Log the exception as needed, depending on your logging strategy
            throw new Exception("An error occurred while retrieving items from the database.", ex);
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>()
        where TEntity : class, IDatabaseModel
    {
        return await GetDbSet<TEntity>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync<TEntity>(string id)
        where TEntity : class, IDatabaseModel
    {
        return await GetDbSet<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public Task UpdateAsync<TEntity>(TEntity entity)
        where TEntity : class, IDatabaseModel
    {
        GetDbSet<TEntity>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task UpdateAsync<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class, IDatabaseModel
    {
        var dbSet = GetDbSet<TEntity>();

        foreach (var entity in entities)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        return Task.CompletedTask;
    }

    public async Task AddAsync<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class, IDatabaseModel
    {
        var dbSet = GetDbSet<TEntity>();

        foreach (var entity in entities)
        {
            var trackedEntity = _context
                .ChangeTracker
                .Entries<TEntity>()
                .FirstOrDefault(e => e.Entity.Id == entity.Id);

            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity.Entity).State = EntityState.Detached;
            }

            await dbSet.AddAsync(entity);
        }
    }
}
