using E_Commerce.application.Repository;
using E_Commerce.DTOs.Product;
using E_Commerce.DTOs.ProductDtos;
using E_Commerce_project.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.application.Interfaces
{
    public interface IProductServices
    {
        public IEnumerable<Product> SearchProducts(string name);
         List<Product> GetAllProduct();

         void AddProduct(Product product);

         void UpdateProduct(Product product);
       
       
         void DeleteProduct(Product product);
         int saveProduct();
        
    }
}
