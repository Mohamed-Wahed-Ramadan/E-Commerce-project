using E_Commerce.application.Repository;
using E_Commerce.Context;
using E_Commerce_project.models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Crypt = BCrypt.Net.BCrypt;

namespace E_commerce.infratructure
{
    public class UserRepository : GenericRepository<User, int>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public bool SignIn(User user, string password)
        {
            return VerifyPassword(password, user.PasswordHash);
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

        public async Task<User?> GetUserByUserNameAsync(string username)
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
            // Generate a salt and hash the password using bcrypt
            // The cost factor of 12 provides a good balance of security and performance
            return Crypt.HashPassword(password);
        }

        // Additional method for verifying passwords
        public bool VerifyPassword(string password, string hash)
        {
            return Crypt.Verify(password, hash);
        }

        public User? GetUserByEmail(string email)
        {
            try
            {
                return _dbContext.Users.SingleOrDefault(u => u.Email.Equals(email));
            }
            catch
            {
                return null;
            }
        }

        public User? GetUserByUserName(string username)
        {
            try
            {
                return _dbContext.Users.SingleOrDefault(u => u.UserName == username);
            }
            catch
            {
                return null;
            }
        }

        public bool IsUserHasMultibleCart(int userId)
        {
            var user = _dbContext.Users.Include(u => u.Carts).FirstOrDefault(u => u.Id == userId);
            return user?.Carts.Count > 1;
        }
    }
}
