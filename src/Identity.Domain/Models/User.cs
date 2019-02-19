using LinFx.Extensions.MultiTenancy;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Models
{
    /// <summary>
    /// Represents a user in the identity system
    /// </summary>
    public class User : IdentityUser, IMultiTenant
    {
        public int? TenantId { set; get; }
    }
}
