using E_Commerce_project.models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.infratructure.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Email)
                .IsRequired().HasMaxLength(100);

            builder.Property(u => u.UserName).IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Password).IsRequired();

            builder.Property(u => u.Role).HasColumnType("nvarchar(50)")
                   .HasConversion<string>();


        }
    }
}
