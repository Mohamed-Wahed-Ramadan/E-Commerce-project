using E_Commerce.application.Contracts;
using E_Commerce_project.models;
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
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }

}
