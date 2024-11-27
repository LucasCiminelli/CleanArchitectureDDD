using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                { 
                    Id = "79ba8e3f-5c28-42cb-a03e-babcfb0b5bd8",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },

                new IdentityRole
                {
                    Id = "8c26c17c-ffe7-43ad-a3b3-b6d50ca71a63",
                    Name = "Operator",
                    NormalizedName = "OPERATOR"
                }


            );
        }
    }
}
