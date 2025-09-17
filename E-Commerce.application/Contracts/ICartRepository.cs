using E_Commerce_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.application.Repository
{
    public interface ICartRepository
    {
        public void DeleteAllUserCarts(int usrId);
        //public void CalculatTotalPrice();
        public Cart? GetCartByUserId(int userId);

        public void AddProductToCart(int cartId, int productId, int quantity);
        public Cart CreateOrUpdateCart(int userId);
        public void DeleteCart(Cart cart);
        public int Save();
    }
}
