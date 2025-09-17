using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.application.Contracts;
using E_Commerce.application.Interfaces;
using E_Commerce.application.Repository;
using E_Commerce_project.models;


namespace E_Commerce.application.Services
{
    public class OrderService : IOrderServices
    {
        private readonly IGenaricRepository<Order, int> _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IGenaricRepository<Order, int> orderRepository, IProductRepository productRepository)
        {
            this._orderRepository = orderRepository;
            this._productRepository = productRepository;
        }

        public Order? CreateOrder(Cart cart)
        {
            if(cart == null)
            {
                return null;
            }
            else if(cart.CartProducts.Count == 0)
            {
                return null;
            }
            var order = new Order
            {
                UserId = cart.UserId,
                OrderDate = DateTime.Now,
            };
            var totalPrice = 0m;
            foreach (var item in cart.CartProducts)
            {
                var product = _productRepository.GetById(item.ProductId);
                if (product.StockQuantity < item.Quantity)
                {
                    item.Quantity = product.StockQuantity;
                }
                product.StockQuantity -= item.Quantity;
                item.Price = product.Price;
                _productRepository.Update(product);
                totalPrice += item.Price * item.Quantity;

                order.ProductOrder.Add(new ProductOrder
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                });
            }
            order.OrderTotalPrice = totalPrice;
            _orderRepository.Add(order);
            Save();
            return order;
        }

        public void DeletOrder(Order order)
        {
            if(order != null)
            {
                _orderRepository.Delete(order);
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        public Order? GetOrderById(int id)
        {
            return _orderRepository.GetById(id);
        }

        public void Save()
        {
            _orderRepository.Complete();
        }
    }
}
