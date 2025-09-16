using E_Commerce.application.Repository;
using E_Commerce.Context;
using E_Commerce_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.infratructure
{
    public class CartRepository : GenericRepository<Cart, int>, ICartRepository
    {
        AppDbContext _context;
        public CartRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Cart> GetAllCarts()
        {
            return _context.Carts.ToList();
        }
        public void CalculatTotalPrice()
        {
            foreach(var cart in _context.Carts)
            {
                var productsPrice = 0m;
                foreach(var product in cart.CartProducts )
                {
                    productsPrice += product.Quantity * product.Product.Price;
                }
                cart.OrderTotalPrice = productsPrice;

            }

        }
        public void AddCart(Cart cart)
        {
            _context.Carts.Add(cart);
        }
        public void UpdateCard(Cart cart)
        {
            _context.Carts.Update(cart);
        }
        public void DelectCart(Cart cart)
        {
            _context.Carts.Remove(cart);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        
    }
}
