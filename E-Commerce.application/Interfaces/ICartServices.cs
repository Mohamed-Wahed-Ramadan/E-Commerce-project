using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_project.models;


namespace E_Commerce.application.Interfaces
{
    public interface ICartServices
    {
        //public List<Cart> GetAllCarts();
        //public void CalculatTotalPrice();
        public Cart GetCartByUserId(int userId);
        public Cart AddProductToCart(int userId, int productId, int quantity);
        //public void CreateOrUpdateCart(int userId);
        public void DeleteCart(Cart cart);
        public int Save();
    }
}
