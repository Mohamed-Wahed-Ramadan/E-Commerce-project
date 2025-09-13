using E_Commerce.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.application.Contracts
{
    public interface IGenaricRepository<T, TId>
    {
        public void Update(T entity);
        public void Delete(T entity);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(TId id);
        void Add(T entity);
        
        Task<int> CompleteAsync();
    }
}
