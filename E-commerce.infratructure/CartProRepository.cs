using E_Commerce.application.Contracts;
using E_Commerce.Context;
using E_Commerce_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.infratructure
{
    internal class CartProRepository: ICartProRepository
    {
        AppDbContext _context;
        public CartProRepository(AppDbContext context) 
        {
            _context = context;
        }

        public void AddCartProduct(Product pro)
        {
            var cartProduct = _context.CartProducts.FirstOrDefault(cp => cp.ProductId == pro.Id);
            if (cartProduct == null)
            {
                _context.CartProducts.Add(
                new CartProduct
                {
                    ProductId = pro.Id,
                    Quantity = 1
                });
            }
            else
            {
                cartProduct.Quantity++;
            }
                
        }


        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
