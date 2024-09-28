using Microsoft.EntityFrameworkCore;
using Movie_Reservation_System.Data;
using Movie_Reservation_System.Interfaces;

namespace Movie_Reservation_System.Repositories;

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

public class BulkRepository(ApplicationDbContext context)
    : IBulkRepository
{
    private readonly ApplicationDbContext _context = context;
    private DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class, IDatabaseModel
        => _context.Set<TEntity>();

    public async Task<int> AddAsync<TEntity>(TEntity entity)
        where TEntity : class, IDatabaseModel
    {
        await GetDbSet<TEntity>().AddAsync(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync<TEntity>(string id)
        where TEntity : class, IDatabaseModel
    {
        var entity = await GetDbSet<TEntity>().FindAsync(id);
        if (entity != null)
        {
            GetDbSet<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>()
        where TEntity : class, IDatabaseModel
    {
        return await GetDbSet<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync<TEntity>(string id)
        where TEntity : class, IDatabaseModel
    {
        return await GetDbSet<TEntity>().FindAsync(id);
    }

    public async void Update<TEntity>(TEntity entity)
        where TEntity : class, IDatabaseModel
    {
        GetDbSet<TEntity>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
