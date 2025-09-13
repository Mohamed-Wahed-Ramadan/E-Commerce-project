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
    internal class CartProRepository: ICartProRepository
    {
        E_commerceContext _context;
        public CartProRepository(E_commerceContext context)
        {
            _context = context;
        }

        public void AddCartProduct(product pro, int quantity,int carId )
        {
            _context.CartProducts.Add(
                new CartProduct
                {
                    CartId = carId,
                    ProductId = pro.Id,
                    Quantity = quantity
                } );
        }


        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
