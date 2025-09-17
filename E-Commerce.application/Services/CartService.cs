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
    public class CartService : ICartServices
    {
        private readonly ICartRepository _cartRepo;
        private readonly IUserServices _userService;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IUserServices userRepository, IProductRepository productRepository)
        {
            _cartRepo = cartRepository;
            this._userService = userRepository;
            _productRepository = productRepository;
        }
        public Cart AddProductToCart(int userId, int productId, int quantity)
        {
            //check if user has a cart
            Cart? cart = _cartRepo.GetCartByUserId(userId);
            if(cart is null)
                cart = _cartRepo.CreateOrUpdateCart(userId);


            var product = _productRepository.GetById(productId);
            if (cart.CartProducts.Any(p => p.ProductId == productId))
            {
                var cartProduct = cart.CartProducts.First(p => p.ProductId == productId);
                int newQuantity = cartProduct.Quantity + quantity;
                if (product.StockQuantity > 0 && product.StockQuantity >= cartProduct.Quantity + quantity)
                {
                    cartProduct.Quantity += quantity;
                }
                else
                {
                    cartProduct.Quantity = product.StockQuantity;
                }
                Save();
                return cart;
            }
            else
            {
                if (product.StockQuantity <= quantity)
                {
                    quantity = product.StockQuantity;
                }
                cart.CartProducts.Add(new CartProduct
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity
                });
            }
            cart.OrderTotalPrice = CalculatTotalPrice(cart.CartProducts);
            return cart;
        }

        public Cart GetCartByUserId(int userId)
        {
            var cart = _cartRepo.GetCartByUserId(userId);
            cart ??= _cartRepo.CreateOrUpdateCart(userId);
            return cart;
        }

        
        private decimal CalculatTotalPrice(IEnumerable<CartProduct> cartProducts)
        {
            decimal totalPrice = 0;
            foreach (var item in cartProducts)
            {
                totalPrice += item.Product.Price * item.Quantity;
            }
            return totalPrice;
        }

        public void DeleteCart(Cart cart)
        {
            _cartRepo.DeleteCart(cart);
        }

        //public List<Cart> GetAllCarts()
        //{
        //    return _cartRepo.GetAllCarts();
        //}

        public int Save()
        {
            return _cartRepo.Save();
        }

        
    }
}
