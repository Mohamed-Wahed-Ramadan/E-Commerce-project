using E_Commerce_project.models;
using E_Commerce_project.models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Context
{
    public class AppDbContext : DbContext
    {

        
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(Connections.DataSource);
            }

            public DbSet<Category> Categories { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<Order> orders { get; set; }
            public DbSet<Cart> carts { get; set; }
            public DbSet<CartProduct> CartProducts { get; set; }
            public DbSet<ProductOrder> ProductOrders { get; set; }
            public DbSet<User> Users { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Category>().HasData(
                   new Category { Id = 1, Name = "Electronics", Description = "Devices and gadgets" },
                   new Category { Id = 2, Name = "Clothes", Description = "Fashion and apparel" },
                   new Category { Id = 3, Name = "Books", Description = "Educational and entertainment books" },
                   new Category { Id = 4, Name = "Home Appliances", Description = "Appliances for home use" },
                   new Category { Id = 5, Name = "Sports", Description = "Sports equipment and accessories" }
               );

                modelBuilder.Entity<Product>().HasData(
                    // Electronics
                    new Product { Id = 1, Name = "Laptop", Description = "High performance laptop", Price = 15000, CategoryId = 1, StockQuantity = 20 },
                    new Product { Id = 2, Name = "Smartphone", Description = "Latest smartphone", Price = 8000, CategoryId = 1, StockQuantity = 50 },
                    new Product { Id = 3, Name = "Headphones", Description = "Wireless headphones", Price = 1200, CategoryId = 1, StockQuantity = 70 },

                    // Clothes
                    new Product { Id = 4, Name = "T-Shirt", Description = "Cotton T-shirt", Price = 250, CategoryId = 2, StockQuantity = 100 },
                    new Product { Id = 5, Name = "Jeans", Description = "Blue denim jeans", Price = 500, CategoryId = 2, StockQuantity = 60 },

                    // Books
                    new Product { Id = 6, Name = "Novel", Description = "Bestselling novel", Price = 120, CategoryId = 3, StockQuantity = 40 },
                    new Product { Id = 7, Name = "Programming Book", Description = "Learn C# from scratch", Price = 300, CategoryId = 3, StockQuantity = 30 },

                    // Home Appliances
                    new Product { Id = 8, Name = "Microwave", Description = "800W microwave oven", Price = 2500, CategoryId = 4, StockQuantity = 15 },
                    new Product { Id = 9, Name = "Vacuum Cleaner", Description = "Bagless vacuum cleaner", Price = 1800, CategoryId = 4, StockQuantity = 25 },

                    // Sports
                    new Product { Id = 10, Name = "Football", Description = "Official size football", Price = 400, CategoryId = 5, StockQuantity = 80 },
                    new Product { Id = 11, Name = "Tennis Racket", Description = "Professional tennis racket", Price = 950, CategoryId = 5, StockQuantity = 20 }
                );
            }

    }


    
}
