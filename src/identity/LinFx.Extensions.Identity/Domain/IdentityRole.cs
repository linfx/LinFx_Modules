using LinFx.Extensions.MultiTenancy;
using System;

namespace LinFx.Extensions.Identity.Domain
{
    /// <summary>
    /// 角色
    /// </summary>
    public class IdentityRole : Microsoft.AspNetCore.Identity.IdentityRole, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public virtual Guid? TenantId { get; set; }

        public IdentityRole() { }

        public IdentityRole(string roleName) : base(roleName) { }
    }
}
