using E_Commerce.application.Contracts;
using E_Commerce.context;
using E_Commerce_project.models;
using Microsoft.EntityFrameworkCore;
using System;
using E_Commerce.context;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.infratructure
{
    
    public class GenaricRepositoties<T, TId> : IGenaricRepository<T, TId> where T : BaseModel<TId>
    {
        E_commerceContext _context;
        DbSet<T> _dbSet;
        public GenaricRepositoties(E_commerceContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            
        }
        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }
        public void Create(T entity)
        {
           _dbSet.Add(entity);  

        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }


        public T GetById(TId pk)
        {
            return _dbSet.Find(pk);
        }

        public int save()
        {
            var ent = _context.ChangeTracker.Entries();
            return _context.SaveChanges();  
//-------------------------------------------------------//
    public class GenericRepository<T, TID> : IGenericRepository<T, TID> where T : BaseModel<TID>
    {
        private protected readonly AppDbContext _dbContext;
        public GenericRepository(AppDbContext dbContext)
        {
           _dbContext = dbContext;   
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
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(TID id)
        {
            return await _dbContext.FindAsync<T>(id);
        }

        public void Update(T entity)
        {
<<<<<<< HEAD
            _dbSet.Update(entity);
            _appDbContext.Update(entity);
=======
            _dbContext.Update(entity);
>>>>>>> origin/master
        }
        public Task<int> CompleteAsync()
        {
            throw new NotImplementedException();
        }
    }

}
