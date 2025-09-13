using E_Commerce.application.Contracts;
using E_Commerce_project.models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.application.Repository
{
    public interface IUserRepository : IGenaricRepository<User,int>
    {
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<User?> GetUserByUsernameAsync(string username);
        public bool SignIn(User user, string password);

        bool IsValidEmail(string email);
        public bool IsValidPassword(string password);
        public string Hash(string password);


    }
}
