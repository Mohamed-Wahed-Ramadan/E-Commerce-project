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

         List<ProductReadDto> GetAllProduct();

         void AddProduct(ProductCreateDto product);

         void UpdateProduct(ProductUpdateDto product);
       
       
         void DeleteProduct(Product product);
         int saveProduct();
        
    }
}
