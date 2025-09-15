using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.DTOs.User;
using E_Commerce_project.models;
using E_Commerce_project.models.User;


namespace E_Commerce.application.Interfaces
{
    public interface IUserServices
    {
        Task<(bool, string)> RegisterAsync(CreateUserDTO user);
        Task<(UserResponse?, string)> LoginAsync(string Email, string Password);
        (bool, string) Register(CreateUserDTO user);
        (UserResponse?, string) Login(string Email, string Password);
    }
}
