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
        public T GetById(TId id);
        public IQueryable<T> GetAll();
        public void Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public int save();

    }
}
