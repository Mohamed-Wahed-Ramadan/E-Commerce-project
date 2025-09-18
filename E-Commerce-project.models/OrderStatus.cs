using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_project.models
{
    public enum OrderStatus
    {
        Pending = 1,
        Processing,
        Shipped,
        Delivered,
        Cancelled,
        Returned
    }
}
