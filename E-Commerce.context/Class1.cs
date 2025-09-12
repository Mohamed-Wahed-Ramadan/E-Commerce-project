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

       // public DbSet<category> categories { get; set; }
    }


}
