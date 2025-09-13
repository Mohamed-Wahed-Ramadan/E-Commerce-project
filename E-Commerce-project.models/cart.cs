using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_project.models.User;
namespace E_Commerce_project.models
{
    public class cart : BaseModel<int>
    {
        public int orderNumber {  get; set; }
        public User user { get; set; }
        public List<CartProduct> CartProducts { get; set; }
        public decimal OrderTotalPrice { get; set; }
    }
}
