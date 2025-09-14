using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.application.Interfaces;
using E_Commerce.application.Repository;
using E_Commerce_project.models;


namespace E_Commerce.application.Services
{
    public class OrderService : IOrderServices
    {
        IOrderRepository OrderRepo;

        public void AddOrder(Cart cart, int orderId)
        {
            throw new NotImplementedException();
        }

        public void DeletOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
