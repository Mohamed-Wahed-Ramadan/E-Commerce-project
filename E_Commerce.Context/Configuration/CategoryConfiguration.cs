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
                 new Category { Id = 1, Name = "Antiquities", Description = "Ancient statues and artifacts" },
                 new Category { Id = 2, Name = "Jewelry", Description = "Precious gems and artifacts" },
                 new Category { Id = 3, Name = "Metals", Description = "Bars and pieces of metal" }
             );
        }
    }
}
