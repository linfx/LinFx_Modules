using LinFx.Extensions.MultiTenancy;
using System;

namespace LinFx.Extensions.Identity.Domain
{
    public class IdentityRole : Microsoft.AspNetCore.Identity.IdentityRole, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
    }
}
