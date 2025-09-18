using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_project.models;


namespace E_Commerce.application.Interfaces
{
    public interface IOrderServices
    {
        public void DeletOrder(Order order);

        public List<Order> GetAllOrders();
        public void AddOrder(Cart cart, int orderId);

        public void Save();
    }
}
