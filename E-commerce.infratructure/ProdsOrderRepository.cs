using E_Commerce.application.Contracts;
using E_Commerce.context;
using E_Commerce_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.infratructure
{
    internal class ProdsOrderRepository: IProdsOrderRepository
    {
        E_commerceContext _context;
        public ProdsOrderRepository(E_commerceContext context)
        {
            _context = context;
        }

        public void AddProductsOrder(cart cart)
        {
            for(int i =0; i< cart.CartProducts.Count; i++)
            {
                _context.ProductOrders.Add
                    (
                     new ProductOrder
                     {
                         OrderId = cart.orderNumber,
                         ProductId = cart.CartProducts[i].ProductId,
                         Quantity = cart.CartProducts[i].Quantity,

                     }
                    );
            }
            
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
