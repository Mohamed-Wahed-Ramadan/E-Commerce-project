using E_Commerce_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.application.Contracts
{
    public interface IProdsOrderRepository
    {
        public void AddProductsOrder(cart cart);
        public void Save();
    }
}
