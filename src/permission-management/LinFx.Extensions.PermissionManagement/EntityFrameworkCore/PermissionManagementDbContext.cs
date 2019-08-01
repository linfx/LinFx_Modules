using Microsoft.EntityFrameworkCore;

namespace LinFx.Extensions.PermissionManagement.EntityFrameworkCore
{
    public class PermissionManagementDbContext : Extensions.EntityFrameworkCore.DbContext
    {
        public DbSet<PermissionGrant> PermissionGrants { get; set; }
    }
}
