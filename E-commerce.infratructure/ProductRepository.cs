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
    public class ProductRepository :GenericRepository<Product,int >, IProductRepository
    {
        AppDbContext _context;//= new();
        public ProductRepository(AppDbContext context):base(context) 
        {
            _context = context;
        }
        #region ProductRepo

        public void Create(Product product)
        {
            _context.Products.Add(product);
        }

        public int Save()
        {
            return _context.SaveChanges();
        } 
        #endregion

    }
}
