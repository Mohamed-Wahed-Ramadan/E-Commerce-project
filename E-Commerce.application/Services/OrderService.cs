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
        private readonly ICartRepository _cartRepository;

        public OrderService(IGenaricRepository<Order, int> orderRepository, IProductRepository productRepository, ICartRepository cartRepository)
        {
            this._orderRepository = orderRepository;
            this._productRepository = productRepository;
            this._cartRepository = cartRepository;
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
                Status = OrderStatus.Pending, // Add default status
                ProductOrder = new List<ProductOrder>() // Initialize collection
            };
            var totalPrice = 0m;
            foreach (var item in cart.CartProducts)
            {
                var product = _productRepository.GetById(item.ProductId);

                // Add proper stock validation
                if (product == null)
                {
                    throw new InvalidOperationException($"Product with ID {item.ProductId} not found.");
                }

                if (product.StockQuantity < item.Quantity)
                {
                    // Instead of silently reducing quantity, throw exception or return error
                    throw new InvalidOperationException($"Insufficient stock for {product.Name}. Available: {product.StockQuantity}, Requested: {item.Quantity}");
                }

                // Update stock
                product.StockQuantity -= item.Quantity;
                _productRepository.Update(product);

                // Calculate price
                var itemPrice = product.Price; // Use product price, not cart item price
                totalPrice += itemPrice * item.Quantity;

                order.ProductOrder.Add(new ProductOrder
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = itemPrice, // Use calculated price
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                });
            }
            order.OrderTotalPrice = totalPrice;
            _orderRepository.Add(order);
            _cartRepository.DeleteCart(cart);
            Save();
            return order;
        }
        private bool ValidateCartStock(Cart cart)
        {
            if (cart?.CartProducts == null) return false;

            foreach (var cartItem in cart.CartProducts)
            {
                var product = _productRepository.GetById(cartItem.ProductId);
                if (product == null || product.StockQuantity < cartItem.Quantity)
                {
                    return false;
                }
            }
            return true;
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

        public void UpdateOrderStatus(int OrderId, OrderStatus status)
        {
            var order = _orderRepository.GetById(OrderId);
            if (order != null)
            {
                order.Status = status;
                _orderRepository.Update(order);
                Save();
            }
        }
    }
}
