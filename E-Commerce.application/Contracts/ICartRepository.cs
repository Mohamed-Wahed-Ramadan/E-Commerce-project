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
        public List<cart> GetAllCarts();
        public void CalculatTotalPrice();
        public void AddCart(cart cart);
        public void DelectCart(cart cart);
        public void Save();
    }
}
