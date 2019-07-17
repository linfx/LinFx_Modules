using LinFx.Extensions.Auditing;
using LinFx.Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LinFx.Identity.EntityFrameworkCore
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole>
    {
        private readonly IAuditPropertySetter _auditPropertySetter = new AuditPropertySetter();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// 租户
        /// </summary>
        public DbSet<Tenant> Tenants { get; set; }

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
