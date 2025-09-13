using E_Commerce.application.Contracts;
using E_Commerce.Context;
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
        AppDbContext _context;
        public ProdsOrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddProductsOrder(Cart cart)
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
