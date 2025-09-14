using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_project.models
{
    public class Product : BaseModel<int>
    {
        //[Key]
        //public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(600)]
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        [MaxLength(600)]

        public string? ImagePath { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        public List<CartProduct> CartProducts { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }


    }
}
