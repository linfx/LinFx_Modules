using LinFx.Extensions.MultiTenancy;

namespace LinFx.Identity.Domain.Models
{
    /// <summary>
    /// Represents a user in the identity system
    /// </summary>
    public class ApplicationUser : IdentityUser, IMultiTenant
    {
    }
}
