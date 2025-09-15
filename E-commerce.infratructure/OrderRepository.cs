using E_Commerce.application.Repository;
using E_Commerce.Context;
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

        public List<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public void AddOrder(Cart cart, int orderId)
        {

            _context.Orders.Add(
                new Order
                {
                    Id = orderId,
                    OrderDate = DateTime.Now,
                    //User = cart.user,
                    OrderTotalPrice = cart.OrderTotalPrice
                }
                );

        }

        public void DeletOrder(Order order)
        {
            _context.Orders.Remove(order);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
