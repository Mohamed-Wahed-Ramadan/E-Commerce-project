using E_Commerce.application.Repository;
using E_Commerce.context;
using E_Commerce_project.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.infratructure
{
    public class OrderRepository : IOrderRepository
    {
        AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<order> GetAllOrders()
        {
            return _context.orders.ToList();
        }

        public void AddOrder(cart cart, int orderId)
        {

            _context.orders.Add(
                new order
                {
                    Id = orderId,
                    OrderDate = DateTime.Now,
                    //User = cart.user,
                    OrderTotalPrice = cart.OrderTotalPrice
                }
                );

        }

        public void DeletOrder(order order)
        {
            _context.orders.Remove(order);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
