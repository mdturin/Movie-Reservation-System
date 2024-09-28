namespace Movie_Reservation_System.Interfaces;

public interface IBulkRepository
{
    Task<int> AddAsync<TEntity>(TEntity entity)
        where TEntity : class, IDatabaseModel;

    Task DeleteAsync<TEntity>(string id)
        where TEntity : class, IDatabaseModel;

    Task<IEnumerable<TEntity>> GetAllAsync<TEntity>()
        where TEntity : class, IDatabaseModel;

    Task<TEntity> GetByIdAsync<TEntity>(string id)
        where TEntity : class, IDatabaseModel;

    void Update<TEntity>(TEntity entity)
        where TEntity : class, IDatabaseModel;
}
