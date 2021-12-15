using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Notes.Identity.Models;

namespace Notes.Identity.Data
{
    public class AuthDbContext : IdentityDbContext<AppUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(x => x.ToTable("Users"));
            builder.Entity<IdentityRole>(x => x.ToTable("Roles"));
            builder.Entity<IdentityUserRole<string>>(x => x.ToTable("UserRoles"));
            builder.Entity<IdentityUserClaim<string>>(x => x.ToTable("UserClaims"));
            builder.Entity<IdentityUserLogin<string>>(x => x.ToTable("UserLogins"));
            builder.Entity<IdentityUserToken<string>>(x => x.ToTable("UserTokens"));
            builder.Entity<IdentityRoleClaim<string>>(x => x.ToTable("RoleClaims"));

            builder.ApplyConfiguration(new AppUserConfiguration());
        }
    }
}
