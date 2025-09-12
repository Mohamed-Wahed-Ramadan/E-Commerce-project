using Microsoft.EntityFrameworkCore;
using E_Commerce_project.models;

namespace E_Commerce.context
{
    public class E_commerceContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Connections.DataSource);
        }
        public DbSet<order> orders { get; set; }
        public DbSet<cart> carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
    }


}
