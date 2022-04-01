using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "f143fd78-6bb1-45da-b2ec-5b1653222d6a",
                    Email = "admin@admin.com",
                    NormalizedEmail = "admin@admin.com",
                    Nombre = "Alejandro",
                    Apellidos = "Ponce",
                    UserName = "alejandro.ponce",
                    NormalizedUserName = "alejandro.ponce",
                    PasswordHash = hasher.HashPassword(null, "secret123"),
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "11f9a10b-3c69-4ce0-bf98-7e468a6c9e5a",
                    Email = "juan.perez@localhost.com",
                    NormalizedEmail = "juan.perez@localhost.com",
                    Nombre = "Juan",
                    Apellidos = "Perez",
                    UserName = "juan.perez",
                    NormalizedUserName = "juan.perez",
                    PasswordHash = hasher.HashPassword(null, "secret123"),
                    EmailConfirmed = true
                }
            );
        }
    }
}
