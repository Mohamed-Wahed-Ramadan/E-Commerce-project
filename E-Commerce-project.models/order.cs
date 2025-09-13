using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_project.models
{
    public class Order : BaseModel<int>
    {
        public List<ProductOrder> ProductOrder { get; set; }
        public User.User User { get; set; }
        public decimal OrderTotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ReceiptDate { get; set; }
    }
}
