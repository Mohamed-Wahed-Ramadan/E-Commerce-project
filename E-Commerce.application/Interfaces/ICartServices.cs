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
        public void AddCart(Cart cart);
        public void DelecCart(Cart cart);
        public void TotalOrderPrice();
        public void Save();

    }
}
