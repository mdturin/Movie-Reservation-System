using System.Linq.Expressions;

namespace Movi.Core.Domain.Interfaces;

public interface IBulkRepository
{
    Task<int> AddAsync<TEntity>(TEntity entity)
        where TEntity : class, IDatabaseModel;

    Task<int> AddAsync<TEntity>(IEnumerable<TEntity> entities)
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

    Task<int> UpdateAsync<TEntity>(TEntity entity)
        where TEntity : class, IDatabaseModel;

    Task<int> UpdateAsync<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class, IDatabaseModel;
}
