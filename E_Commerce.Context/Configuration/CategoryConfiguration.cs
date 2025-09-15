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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                   new Category { Id = 1, Name = "Electronics", Description = "Devices and gadgets" },
                   new Category { Id = 2, Name = "Clothes", Description = "Fashion and apparel" },
                   new Category { Id = 3, Name = "Books", Description = "Educational and entertainment books" },
                   new Category { Id = 4, Name = "Home Appliances", Description = "Appliances for home use" },
                   new Category { Id = 5, Name = "Sports", Description = "Sports equipment and accessories" }
               );
        }
    }
}
