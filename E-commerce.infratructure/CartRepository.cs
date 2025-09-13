using E_Commerce.application.Repository;
using E_Commerce.context;
using E_Commerce_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.infratructure
{
    public class CartRepository : ICartRepository
    {
        AppDbContext _context;
        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<cart> GetAllCarts()
        {
            return _context.carts.ToList();
        }
        public void CalculatTotalPrice()
        {
            foreach(var cart in _context.carts)
            {
                var productsPrice = 0m;
                foreach(var product in cart.CartProducts )
                {
                    productsPrice += product.Quantity * product.Product.Price;
                }
                cart.OrderTotalPrice = productsPrice;

            }

        }
        public void AddCart(cart cart)
        {
            _context.carts.Add(cart);
        }
        public void DelectCart(cart cart)
        {
            _context.carts.Remove(cart);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        
    }
}
