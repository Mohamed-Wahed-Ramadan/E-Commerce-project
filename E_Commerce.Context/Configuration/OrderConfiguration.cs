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
    internal class OrderConfiguration :IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o=> o.Id);
            builder.HasMany(o => o.ProductOrders).WithOne(po=> po.Order).HasForeignKey(po=> po.OrderId);
            builder.Property(o => o.OrderTotalPrice).IsRequired();
            builder.Property(o => o.OrderDate).IsRequired();
        }

        
    }
}
