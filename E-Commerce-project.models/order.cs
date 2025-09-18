using E_Commerce_project.models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_project.models
{
    public class Order : BaseModel<int>
    {
        public decimal OrderTotalPrice { get; set; }
        public OrderStatus Status { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public int UserId { get; set; }
        public User.User User { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
        //public global::WinForms.pressentation.Form1.OrderStatus Status { get; set; }
    }
}
