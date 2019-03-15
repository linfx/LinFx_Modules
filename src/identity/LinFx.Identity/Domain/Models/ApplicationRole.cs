using LinFx.Extensions.MultiTenancy;

namespace LinFx.Identity.Domain.Models
{
    /// <summary>
    /// Represents a role in the identity system
    /// </summary>
    public class ApplicationRole : IdentityRole, IMultiTenant
    {
        public ApplicationRole() { }

        public ApplicationRole(string roleName) : base(roleName) { }
    }
}