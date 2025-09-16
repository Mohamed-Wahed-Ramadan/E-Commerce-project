using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_project.models.User
{
    public class User : BaseModel<int>
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string? FullName { get; set; }
        public UserRole Role { get; init; } = UserRole.Customer;
        public ICollection<Order> Orders { get; set; }

        public ICollection<Cart> Carts { get; set; }

    }
}
