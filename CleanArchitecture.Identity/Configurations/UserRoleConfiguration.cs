using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "6c665b18-52c9-4b06-ae9d-6bf9a5828660",
                    UserId = "f143fd78-6bb1-45da-b2ec-5b1653222d6a"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "6578ff56-5b02-487a-8610-afaafcf98777",
                    UserId = "11f9a10b-3c69-4ce0-bf98-7e468a6c9e5a"
                }
            );
        }
    }
}
