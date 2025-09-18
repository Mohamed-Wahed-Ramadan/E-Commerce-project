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
        IOrderRepository _orderRepo;

        public void AddOrder(Cart cart, int orderId)
        {
            _orderRepo.AddOrder(cart,orderId);
        }
        
        public void DeletOrder(Order order)
        {
            _orderRepo.DeletOrder(order);
        }

        public List<Order> GetAllOrders()
        {
           return _orderRepo.GetAllOrders();
        }

        public void Save()
        {
            _orderRepo.Save();
        }
    }
}
