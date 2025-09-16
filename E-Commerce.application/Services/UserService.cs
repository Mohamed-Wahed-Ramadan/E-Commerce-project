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

        public (UserResponse?, string) Login(string Email, string Password)
        {
            User? user =  _userRepository.GetUserByEmail(Email);
            if (user is null)
                return (null, "Email is incorrect!");

            var result = _userRepository.SignIn(user, Password);

            if (!result)
                return (null, "Password is in Correct!");

            return (user?.ToUserResponse(), "Success!");
        }

        public async Task<(UserResponse?, string)> LoginAsync(string Email, string Password)
        {
            User? user = await _userRepository.GetUserByEmailAsync(Email);
            if (user is null)
                return (null, "Email is incorrect!");

            var result = _userRepository.SignIn(user, Password);

            if (!result)
                return (null, "Password is in Correct!");

            return (user?.ToUserResponse(), "Success!");


        }

        public (bool, string) Register(CreateUserDTO user)
        {
            var userExist =  _userRepository.GetUserByEmail(user.Email);
            if (userExist is not null) return (false, "Email is already Exist!");

            userExist = _userRepository.GetUserByUserName(user.UserName);
            if (userExist is not null) return (false, "UserName is Already Taken!");

            if (!_userRepository.IsValidEmail(user.Email)) return (false, "Invalid EmailFormat");

            if (!_userRepository.IsValidPassword(user.Password)) return (false, "Password Must be Minimum 4 chars!");


            _userRepository.Add(user.ToUser());

            return (true, "you Succesfully Registered!");
        }

        public async Task<(bool,string)> RegisterAsync(CreateUserDTO userRequest)
        {
            var userExist = await _userRepository.GetUserByEmailAsync(userRequest.Email);
            if (userExist is not null) return (false, "Email is already Exist!");

            userExist = await _userRepository.GetUserByUserNameAsync(userRequest.UserName);
            if (userExist is not null) return (false, "UserName is Already Taken!");

            if (!_userRepository.IsValidEmail(userRequest.Email)) return (false, "Invalid EmailFormat");

            if (!_userRepository.IsValidPassword(userRequest.Password)) return (false, "Password Must be Minimum 4 chars!");


            _userRepository.Add(userRequest.ToUser());

            return (true, "you Succesfully Registered!");
            
        }
    }
}
