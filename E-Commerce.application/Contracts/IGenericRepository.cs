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

        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll();
        Task<T?> GetByIdAsync(TId id);
        T? GetById(TId id);
        void Add(T entity);
        void Update(T entity); 
        void Delete(T entity);
        int CompleteAsync();
    }
}
