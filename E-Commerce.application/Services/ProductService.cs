using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.application.Interfaces;
using E_Commerce.application.Repository;
using E_Commerce_project.models;


namespace E_Commerce.application.Services
{
    public class ProductService : IProductServices
    {
        IProductRepository _context;
        public ProductService(IProductRepository context)
        {
            _context = context;
        }
        public IQueryable<Product> GetAllProduct()
        {
            return _context.GetAll();
        }
        public void AddProduct(Product product)
        {
            _context.Create(product);

        }
        public void UpdateProduct(Product product)
        {
            _context.Update(product);
        }
        public void DeleteProduct(Product product)
        {
            _context.Delete(product);
        }
        public int saveProduct()
        {
            return _context.Save();
        }
    }
}
