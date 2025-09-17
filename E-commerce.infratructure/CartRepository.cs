using E_Commerce.application.Repository;
using E_Commerce.Context;
using E_Commerce_project.models;
using Microsoft.EntityFrameworkCore;
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

        public void AddProductToCart(int cartId, int productId, int quantity)
        {
            _context.CartProducts.Add(new CartProduct
            {
                CartId = cartId,
                ProductId = productId,
                Quantity = quantity
            });
        }

        public Cart CreateOrUpdateCart(int userId)
        {
            var cart = GetCartByUserId(userId);
            cart ??= new Cart
                {
                    UserId = userId,
                    OrderTotalPrice = 0,
                    CartProducts = new List<CartProduct>()
                };
            _context.Carts.Add(cart);
            Save();
            return cart;
        }

        public void DeleteAllUserCarts(int usrId)
        {
            var carts = _context.Carts.Where(c => c.UserId == usrId).ToList();
            foreach (var cart in carts)
            {
                _context.Carts.Remove(cart);
            }
        }

        public void DeleteCart(Cart cart)
        {
            _context.Carts.Remove(cart);
        }

        public Cart? GetCartByUserId(int userId)
        {
            try
            {
                return _context.Carts.Where(c => c.UserId == userId)
                                     .Include(c => c.User)
                                     .Include(c => c.CartProducts)
                                     .ThenInclude(cp => cp.Product)
                                     .SingleOrDefault();
            }
            catch
            {
                return null;
            }
        }


        public int Save()
        {
            return _context.SaveChanges();
        }
        
    }
}
