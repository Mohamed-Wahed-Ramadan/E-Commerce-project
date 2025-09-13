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
    public class GenericRepository<T, TID> : IGenaricRepository<T, TID> where T : BaseModel<TID>
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
            _dbContext.Update(entity);
        }
        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }

}
