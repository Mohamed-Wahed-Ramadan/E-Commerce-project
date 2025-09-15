using E_Commerce.application.Contracts;
using E_Commerce.application.Interfaces;
using E_Commerce.application.Repository;
using E_Commerce.DTOs.CategoryDtos;
using E_Commerce.DTOs.Product;
using E_Commerce.DTOs.ProductDtos;
using E_Commerce_project.models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.application.Services
{
    public class ProductService : IProductServices
    {
        //IProductRepository _context;
        IGenaricRepository<Product, int> _context;
        public ProductService(IGenaricRepository<Product, int> context)
        {
            _context = context;
        }
        public List<ProductReadDto> GetAllProduct()
        {
            IQueryable<Product> AllProduct = _context.GetAll();
            var Allprod= AllProduct.ToList().Adapt<List<ProductReadDto>>();

            return Allprod;
        }
        public void AddProduct(ProductReadDto productC)
        {
            Product product1 = productC.Adapt<Product>();
            _context.Add(product1);

        }
        public void UpdateProduct(ProductUpdateDto productU)
        {
            Product product2 = productU.Adapt<Product>();
            _context.Update(product2);
        }
        public void DeleteProduct(Product product)
        {
            _context.Delete(product);
        }
        public int saveProduct()
        {
            return _context.CompleteAsync();
        }
    }
}
