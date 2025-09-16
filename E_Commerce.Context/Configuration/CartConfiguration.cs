using E_Commerce_project.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Context.Configuration
{
    public class CartConfiguration :IEntityTypeConfiguration<Cart>
    {
        

        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(c => c.CartProducts)
                   .WithOne(cp => cp.Cart)
                   .HasForeignKey(cp => cp.CartId);

            builder.HasOne(c => c.User)
                   .WithMany(u => u.Carts)
                   .HasForeignKey(c => c.UserId);
        }
    }
}
