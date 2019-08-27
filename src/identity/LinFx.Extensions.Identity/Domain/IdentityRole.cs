using LinFx.Extensions.MultiTenancy;

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
        public virtual string TenantId { get; set; }

        public IdentityRole() { }

        public IdentityRole(string roleName) : base(roleName) { }
    }
}
