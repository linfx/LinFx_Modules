using LinFx.Extensions.MultiTenancy;
using System;

namespace LinFx.Extensions.Identity.Domain
{
    /// <summary>
    /// 角色
    /// </summary>
    public class IdentityRole : IdentityRole<string>
    {
        public IdentityRole() { }

        public IdentityRole(string roleName) : base(roleName) { }
    }

    /// <summary>
    /// 角色
    /// </summary>
    public class IdentityRole<TKey> : Microsoft.AspNetCore.Identity.IdentityRole<TKey>, IMultiTenant
         where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public virtual string TenantId { get; set; }

        public IdentityRole() { }

        public IdentityRole(string roleName) : base(roleName) { }
    }
}
