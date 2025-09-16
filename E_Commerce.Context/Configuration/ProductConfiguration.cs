using E_Commerce_project.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Context.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId);

            builder.HasData(
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
