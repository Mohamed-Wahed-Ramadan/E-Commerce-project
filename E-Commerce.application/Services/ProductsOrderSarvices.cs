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
    public class ProductsOrderSarvices: IProductsOrderServices
    {
        IProdsOrderRepository _proOrderRepo;

        public void AddProductsOrder(Cart cart)
        {
            _proOrderRepo.AddProductsOrder(cart);
        }

        public void Save()
        {
            _proOrderRepo.Save();
        }
    }
}
