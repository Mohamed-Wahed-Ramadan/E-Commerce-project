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
        private readonly AppDbContext _appDbContext;
        public GenericRepository(AppDbContext dbContext)
        {
           _appDbContext = dbContext;   
        }
        public void Add(T entity)
        {
            _appDbContext.Add(entity);
        }

        

        public void Delete(T entity)
        {
            _appDbContext.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(TID id)
        {
            return await _appDbContext.FindAsync<T>(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _appDbContext.Update(entity);
        }
        public Task<int> CompleteAsync()
        {
            throw new NotImplementedException();
        }
    }

}
