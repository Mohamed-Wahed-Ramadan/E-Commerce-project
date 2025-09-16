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
        public List<Cart> GetAllCarts();
        public void CalculatTotalPrice();
        public void AddCart(Cart cart);
        public void UpdateCard(Cart cart);
        public void DelectCart(Cart cart);
        public void Save();
    }
}
