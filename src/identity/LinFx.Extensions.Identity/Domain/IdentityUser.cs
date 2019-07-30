using LinFx.Extensions.MultiTenancy;
using System;

namespace LinFx.Extensions.Identity.Domain
{
    public class IdentityUser : Microsoft.AspNetCore.Identity.IdentityUser, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { set; get; }
    }
}
