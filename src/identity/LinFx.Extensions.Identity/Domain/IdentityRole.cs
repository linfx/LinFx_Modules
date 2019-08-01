using LinFx.Extensions.MultiTenancy;
using System;

namespace LinFx.Extensions.Identity.Domain
{
    public class IdentityRole : Microsoft.AspNetCore.Identity.IdentityRole, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public IdentityRole() { }

        public IdentityRole(string roleName) : base(roleName) { }
    }
}
