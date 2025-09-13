using E_Commerce.application.Repository;
using E_Commerce.context;
using E_Commerce_project.models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace E_commerce.infratructure
{
    public class UserRepository : GenericRepository<User, int>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public bool SignIn(User user, string password)
        {
            return user.PasswordHash.Equals(password) ? true : false;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email.Equals(email));
            }
            catch
            {
                return null;
            }
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            try
            {
                return await _dbContext.Users.SingleOrDefaultAsync(u => u.UserName == username);
            }
            catch
            {
                return null;
            }
        }

        public bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
        }

        public bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, "^.{4,}$");
        }
        public string Hash(string password)
        {
            return password.GetHashCode().ToString();
        }
    }
}
