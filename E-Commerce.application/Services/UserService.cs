using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.application.Interfaces;
using E_Commerce.application.Mapper;
using E_Commerce.application.Repository;
using E_Commerce.DTOs.User;
using E_Commerce_project.models;
using E_Commerce_project.models.User;


namespace E_Commerce.application.Services
{
    public class UserService(IUserRepository userRepository) : IUserServices
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<(UserResponse?, string)> LoginAsync(string Email, string Password)
        {
            User? user = await _userRepository.GetUserByEmailAsync(Email);
            if (user is null || string.IsNullOrWhiteSpace(Password))
                return (user?.ToUserResponse(), "Email Or Password is incorrect!");

            if (user.PasswordHash != _userRepository.Hash(Password))
                return (null, "Password is in Correct!");

            return (user?.ToUserResponse(), "Success!");


        }

        public async Task<(bool,string)> RegisterAsync(CreateUserDTO user)
        {
            var userExist = await _userRepository.GetUserByEmailAsync(user.Email);
            if (userExist is not null) return (false, "Email is already Exist!");

            userExist = await _userRepository.GetUserByUsernameAsync(user.UserName);
            if (userExist is not null) return (false, "UserName is Already Taken!");

            if (!_userRepository.IsValidEmail(user.Email)) return (false, "Invalid EmailFormat");

            if (!_userRepository.IsValidPassword(user.Password)) return (false, "Password Must be Minimum 4 chars!");


            _userRepository.Add(user.ToUser());

            return (true, "you Succesfully Registered!");
            
        }
    }
}
