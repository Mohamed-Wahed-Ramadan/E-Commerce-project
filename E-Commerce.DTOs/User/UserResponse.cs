using E_Commerce_project.models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DTOs.User
{
    public class UserResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole Role  { get; set; }
    }
}
