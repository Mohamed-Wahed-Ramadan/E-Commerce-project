using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.application.Interfaces;
using E_Commerce.application.Repository;
using E_Commerce_project.models;


namespace E_Commerce.application.Services
{
    public class CartService : ICartServices
    {
        ICartRepository _cartRepo;

        public List<Cart> GetAllCarts()
        {
            return _cartRepo.GetAllCarts();
        }

        public void AddCart(Cart cart)
        {
            _cartRepo.AddCart(cart);
        }

        public void DelecCart(Cart cart)
        {
            _cartRepo.DelectCart(cart);
        }

        public void TotalOrderPrice()
        {
            _cartRepo.CalculatTotalPrice();
        }

        public void Save()
        {
            _cartRepo.Save();
        }
    }
}
