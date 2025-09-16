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
        public List<Cart> GetAllCarts();
        public void CalculatTotalPrice();
        public void AddCart(Cart cart);
        public void UpdateCard(Cart cart);

        public void DelectCart(Cart cart);
        public void Save();
    }
}
