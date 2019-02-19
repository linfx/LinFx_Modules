using LinFx.Extensions.MultiTenancy;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Models
{
    /// <summary>
    /// Represents a role in the identity system
    /// </summary>
    public class Role : IdentityRole, IMultiTenant
    {
        public int? TenantId { set; get; }
    }
}