using E_Commerce.application.Contracts;
using E_Commerce.context;
using E_Commerce_project.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.infratructure
{
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
            _appDbContext.Update(entity);
        }
        public Task<int> CompleteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
