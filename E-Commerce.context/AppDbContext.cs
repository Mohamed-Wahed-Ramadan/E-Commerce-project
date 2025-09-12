using Microsoft.EntityFrameworkCore;
using E_Commerce_project.models;
using E_Commerce_project.models.User;

namespace E_Commerce.context
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Connections.DataSource);
        }

        // public DbSet<category> categories { get; set; }
        public DbSet<User> Users { get; set; }

    }


}
