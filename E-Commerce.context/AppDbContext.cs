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
<<<<<<< HEAD:E-Commerce.context/AppDbContext.cs

        // public DbSet<category> categories { get; set; }
        public DbSet<User> Users { get; set; }

=======
        public DbSet<order> orders { get; set; }
        public DbSet<cart> carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
>>>>>>> origin/nedaa:E-Commerce.context/Class1.cs
    }


}
