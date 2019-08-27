using LinFx.Extensions.Auditing;
using LinFx.Extensions.Identity.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LinFx.Extensions.Identity.EntityFrameworkCore
{
    public class IdentityDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    public class IdentityDbContext<TUser, TRole> : IdentityDbContext<TUser, TRole, string>
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
    }
}
