using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.application.Contracts
{
    public interface IGenericRepository<T,TID>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(TID id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<int> CompleteAsync();
    }
}
