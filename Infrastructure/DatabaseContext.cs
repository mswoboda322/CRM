using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DatabaseContext : IdentityDbContext<User, IdentityRole<long>, long>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        // TODO: We need to verify if still need it
    }

    protected DatabaseContext()
    {
    }

    // NOTE: Place DbSets properties here, example:
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);

        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("Users");
        builder.Entity<IdentityUserClaim<long>>().ToTable("UserClaims");
        builder.Entity<IdentityUserLogin<long>>().ToTable("UserLogins");
        builder.Entity<IdentityUserToken<long>>().ToTable("UserTokens");
        builder.Entity<IdentityRole<long>>().ToTable("Roles");
        builder.Entity<IdentityRoleClaim<long>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserRole<long>>().ToTable("UserRoles");
    }
}
