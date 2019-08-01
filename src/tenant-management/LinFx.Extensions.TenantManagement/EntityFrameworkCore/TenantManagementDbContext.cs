using LinFx.Extensions.TenantManagement.Domain;
using Microsoft.EntityFrameworkCore;
using DbContext = LinFx.Extensions.EntityFrameworkCore.DbContext;

namespace LinFx.Extensions.TenantManagement.EntityFrameworkCore
{
    public class TenantManagementDbContext : DbContext
    {
        public TenantManagementDbContext(DbContextOptions<TenantManagementDbContext> options) : base(options) { }

        public DbSet<Tenant> Tenants { get; set; }
    }
}
