using E_Commerce.DTOs.User;
using E_Commerce_project.models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.application.Mapper
{
    public static class UserMapping
    {
        public static UserResponse ToUserResponse(this User user)
        {
            return new UserResponse
            {
                Name = user.UserName,
                Email = user.Email,
                Role = user.Role,
            };
        }
        public static User ToUser(this CreateUserDTO userDto)
        {
            return new User
            {
               UserName = userDto.UserName,
               Email = userDto.Email,
               PasswordHash = userDto.Password.GetHashCode().ToString(),
            };
        }
    }
}
