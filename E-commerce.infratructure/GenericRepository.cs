using E_Commerce.application.Contracts;
using E_Commerce.Context;
using E_Commerce_project.models;
using Microsoft.EntityFrameworkCore;

public class GenericRepository<T, TID> : IGenaricRepository<T, TID> where T : BaseModel<TID>
{
    private protected readonly AppDbContext _dbContext;
    private protected readonly DbSet<T> _dbSet; 
    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet=_dbContext.Set<T>();
    }
    public void Add(T entity)
    {
        _dbContext.Add(entity);
    }



    public void Delete(T entity)
    {
        _dbContext.Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(TID id)
    {
        return await _dbContext.FindAsync<T>(id); 
    }

    public void Update(T entity)
    {
        _dbContext.Update(entity);
    }
    public int CompleteAsync()
    {
        return _dbContext.SaveChanges();
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T? GetById(TID id)
    {
        return _dbSet.Find(id);
    }
}