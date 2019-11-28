using Microsoft.EntityFrameworkCore;
using DbContext = LinFx.Data.DbContext;

namespace LinFx.Extensions.PermissionManagement.Data
{
    public class PermissionManagementDbContext : DbContext
    {
        public PermissionManagementDbContext(DbContextOptions<PermissionManagementDbContext> options) : base(options) { }

        public DbSet<PermissionGrant> PermissionGrants { get; set; }
    }
}
