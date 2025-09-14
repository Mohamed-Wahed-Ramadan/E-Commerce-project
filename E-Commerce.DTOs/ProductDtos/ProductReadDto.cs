using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DTOs.ProductDtos
{
    public class ProductReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        //public string CategoryName { get; set; }  // ناخد اسم الكاتجوري بدل الـ Id
        public string ImagePath { get; set; }
        public int StockQuantity { get; set; }

    }
}
