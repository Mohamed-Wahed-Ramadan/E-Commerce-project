using E_Commerce_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.application.Interfaces
{
    public interface IProductsOrderServices
    {
        public void AddProductsOrder(Cart cart);
        public void Save();
    }
}
