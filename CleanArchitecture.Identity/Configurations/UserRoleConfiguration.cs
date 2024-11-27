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
                    RoleId = "79ba8e3f-5c28-42cb-a03e-babcfb0b5bd8",
                    UserId = "f284b3fd-f2cf-476e-a9b6-6560689cc48c"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "8c26c17c-ffe7-43ad-a3b3-b6d50ca71a63",
                    UserId = "294d249b-9b57-48c1-9689-11a91abb6447"
                }

            );
        }
    }
}
