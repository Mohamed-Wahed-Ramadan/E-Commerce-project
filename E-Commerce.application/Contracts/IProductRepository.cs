using E_Commerce_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.application.Repository
{
    public interface IProductRepository
    {
        public IQueryable<Product> GetAll();
        public Product GetById(int id);
        public void Create(Product product );
        public void Update(Product product );
        public void Delete(Product product );
        public int Save();
    }
}
