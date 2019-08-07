using Microsoft.EntityFrameworkCore;

namespace LinFx.Extensions.PermissionManagement.EntityFrameworkCore
{
    public class PermissionManagementDbContext : Extensions.EntityFrameworkCore.DbContext
    {
        public PermissionManagementDbContext(DbContextOptions<PermissionManagementDbContext> options) : base(options) { }

        public DbSet<PermissionGrant> PermissionGrants { get; set; }
    }
}
