using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Identity
{
    public class CleanArchitectureIdentityDbContext : IdentityDbContext
    {
        public CleanArchitectureIdentityDbContext(DbContextOptions<CleanArchitectureIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


        }

        public virtual DbSet<RefreshToken>? RefreshTokens { get; set; }
        public virtual DbSet<ApplicationUser>? ApplicationUsers { get; set; }

    }
}
