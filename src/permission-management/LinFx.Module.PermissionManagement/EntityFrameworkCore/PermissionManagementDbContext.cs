using LinFx.Extensions.PermissionManagement.Domain;
using Microsoft.EntityFrameworkCore;
using DbContext = LinFx.EntityFrameworkCore.DbContext;

namespace LinFx.Extensions.PermissionManagement.EntityFrameworkCore
{
    public class PermissionManagementDbContext : DbContext
    {
        public PermissionManagementDbContext(DbContextOptions<PermissionManagementDbContext> options)
            : base(options) { }

        /// <summary>
        /// 权限授权
        /// </summary>
        public DbSet<PermissionGrant> PermissionGrants { get; set; }
    }
}
