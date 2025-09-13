using E_Commerce.application.Repository;
using E_Commerce.application.Services;
using E_Commerce.Context;
using E_Commerce_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.infratructure
{
    public class ProductRepository : IProductRepository
    {
        AppDbContext _context;//= new();
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<Product> GetAll()
        {
            return _context.Products;
        }
        public void Create(Product product)
        {
            _context.Products.Add(product);
        }
        public void Update(Product product)
        {
            _context.Products.Update(product);
        }


        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }
        public int Save()
        {
            return _context.SaveChanges();
        }

    }
}
