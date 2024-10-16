using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Movi.Core.Domain.Interfaces;

public interface IBulkRepository
{
    IDbContextTransaction BeginTransaction();

    Task<int> SaveChangesAsync();

    Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class, IDatabaseModel;

    Task AddAsync<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class, IDatabaseModel;

    Task DeleteAsync<TEntity>(string id)
        where TEntity : class, IDatabaseModel;

    Task DeleteAsync<TEntity>(IEnumerable<string> ids)
        where TEntity : class, IDatabaseModel;

    Task<TEntity> GetItemAsync<TEntity>(
        Expression<Func<TEntity, bool>> conditionExpression,
        params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IDatabaseModel;

    Task<List<TEntity>> GetItemsAsync<TEntity>(
        Expression<Func<TEntity, bool>> conditionExpression,
        params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IDatabaseModel;

    Task<IEnumerable<TEntity>> GetAllAsync<TEntity>()
        where TEntity : class, IDatabaseModel;

    Task<TEntity> GetByIdAsync<TEntity>(string id)
        where TEntity : class, IDatabaseModel;

    Task UpdateAsync<TEntity>(TEntity entity)
        where TEntity : class, IDatabaseModel;

    Task UpdateAsync<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class, IDatabaseModel;
}
