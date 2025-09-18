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
     // Antiquities
     new Product { Id = 1, Name = "Tutankhamun's Mask", Description = "A replica of the famous golden mask of Tutankhamun.", Price = 5000, CategoryId = 1, StockQuantity = 10, ImagePath = "images/a1.png" },
     new Product { Id = 2, Name = "Pharaonic Statue", Description = "A statue of a high-ranking official from ancient Egypt.", Price = 3500, CategoryId = 1, StockQuantity = 15, ImagePath = "images/a2.png" },
     new Product { Id = 3, Name = "Horus Falcon Statue", Description = "A statue of the falcon god Horus.", Price = 4500, CategoryId = 1, StockQuantity = 12, ImagePath = "images/a3.png" },
     new Product { Id = 4, Name = "Winged Lion Statue", Description = "A statue of a winged lion from ancient mythology.", Price = 4200, CategoryId = 1, StockQuantity = 8, ImagePath = "images/a4.png" },
     new Product { Id = 5, Name = "Ankh (Key of Life)", Description = "An ankh, the ancient Egyptian hieroglyphic symbol for life.", Price = 2000, CategoryId = 1, StockQuantity = 20, ImagePath = "images/a5.png" },
     new Product { Id = 6, Name = "Queen Nefertiti Bust", Description = "A bust of the Great Royal Wife of Pharaoh Akhenaten.", Price = 5500, CategoryId = 1, StockQuantity = 7, ImagePath = "images/a6.png" },
     new Product { Id = 7, Name = "Sphinx Statue", Description = "A replica of the iconic Great Sphinx of Giza.", Price = 4800, CategoryId = 1, StockQuantity = 10, ImagePath = "images/a7.png" },

     // Metals
     new Product { Id = 8, Name = "Gold Bars", Description = "Pure gold bullion bars.", Price = 100000, CategoryId = 3, StockQuantity = 5, ImagePath = "images/b1.png" },
     new Product { Id = 9, Name = "Small Gold Bars", Description = "Small pure gold bars.", Price = 50000, CategoryId = 3, StockQuantity = 8, ImagePath = "images/b2.png" },

     // Jewelry
     new Product { Id = 10, Name = "Red Ruby Gem", Description = "A natural, uncut red ruby.", Price = 25000, CategoryId = 2, StockQuantity = 3, ImagePath = "images/c1.png" },
     new Product { Id = 11, Name = "Raw Diamond Gem", Description = "A natural, uncut raw diamond.", Price = 120000, CategoryId = 2, StockQuantity = 2, ImagePath = "images/c2.png" }
 );
        }
    }
}
