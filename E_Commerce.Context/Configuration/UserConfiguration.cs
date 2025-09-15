using E_Commerce_project.models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Context.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.UserName)
                   .IsRequired()
                   .HasMaxLength(150);
            builder.Property(u => u.PasswordHash)
                   .IsRequired();
            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(300);
            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(300);

            builder.Property(u => u.Role)
                   .IsRequired()
                   .HasConversion<string>();

            
             //builder.HasData(
             //       new User{ Id = 1, FullName = "Admin", Email = "admin@iti.eg",
             //                   PasswordHash = "admin".GetHashCode().ToString(),
             //                   Role = UserRole.Admin, UserName = "admin" }
             //   );
             




        }
    }
}
