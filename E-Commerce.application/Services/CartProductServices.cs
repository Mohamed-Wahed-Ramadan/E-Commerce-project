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
    public class CartProductServices: ICartProServices
    {
        ICartProRepository _cartProRepo;

        public void AddCartProduct(Product pro)
        {
            _cartProRepo.AddCartProduct(pro);
        }

        public void Save()
        {
            _cartProRepo.Save();
        }
    }
}
