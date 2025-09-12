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
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public UserRole Role { get; init; } = UserRole.Customer;

        public List<order> MyProperty { get; set; }

        public UserRole GetRole() { return Role; }
    }
}
