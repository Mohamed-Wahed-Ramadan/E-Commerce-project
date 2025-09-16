using E_Commerce.application.Contracts;
using E_Commerce.application.Interfaces;
using E_Commerce_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.application.Services
{
    internal class ProductsOrderServices : IProductsOrderServices
    {
        IProdsOrderRepository _prodsOrderRepo;
        public void AddProductsOrder(Cart cart)
        {
            _prodsOrderRepo.AddProductsOrder(cart);
        }

        public void Save()
        {
            _prodsOrderRepo.Save();
        }
    }
}
