using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce_project.models
{
    public class Cart : BaseModel<int>
    {
        public int orderNumber {  get; set; }
        public User.User User { get; set; }
        public List<CartProduct> CartProducts { get; set; }
        public decimal OrderTotalPrice { get; set; }
    }
}
