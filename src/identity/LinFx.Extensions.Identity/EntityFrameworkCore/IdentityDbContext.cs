using LinFx.Extensions.Auditing;
using LinFx.Extensions.Identity.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LinFx.Extensions.Identity.EntityFrameworkCore
{
    public class IdentityDbContext : IdentityDbContext<IdentityUser, IdentityRole>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    public class IdentityDbContext<TUser, TRole> : IdentityDbContext<TUser, TRole, string, IdentityUserClaim, IdentityUserRole, IdentityUserLogin, IdentityRoleClaim, IdentityUserToken>
        where TUser : IdentityUser
        where TRole : IdentityRole
    {
        private readonly IAuditPropertySetter _auditPropertySetter = new AuditPropertySetter(null, null);

        public IdentityDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TUser>(b =>
            {
                b.Property(u => u.TenantId).HasMaxLength(32);
            });

            builder.Entity<TRole>(b =>
            {
                b.Property(u => u.TenantId).HasMaxLength(32);
            });

            ConfigureTable(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected virtual void OnBeforeSaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        _auditPropertySetter.SetCreationProperties(entry.Entity);
                        break;
                    case EntityState.Modified:
                        _auditPropertySetter.SetModificationProperties(entry.Entity);
                        break;
                    case EntityState.Deleted:
                        _auditPropertySetter.SetDeletionProperties(entry.Entity);
                        break;
                }
            }
        }

        protected virtual void ConfigureTable(ModelBuilder builder)
        {
            builder.Entity<TUser>().ToTable(TableConsts.IdentityUsers);
            builder.Entity<TRole>().ToTable(TableConsts.IdentityRoles);
            builder.Entity<IdentityUserRole>().ToTable(TableConsts.IdentityUserRoles);
            builder.Entity<IdentityRoleClaim>().ToTable(TableConsts.IdentityRoleClaims);
            builder.Entity<IdentityUserLogin>().ToTable(TableConsts.IdentityUserLogins);
            builder.Entity<IdentityUserClaim>().ToTable(TableConsts.IdentityUserClaims);
            builder.Entity<IdentityUserToken>().ToTable(TableConsts.IdentityUserTokens);
        }
    }
}
