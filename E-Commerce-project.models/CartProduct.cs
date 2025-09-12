using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_project.models
{
    public class CartProduct
    {
        public int CartId { get; set; }
        public cart Cart { get; set; }
        public int ProductId { get; set; }
        public product Product { get; set; }
        public int Quantity { get; set; }
    }
}
