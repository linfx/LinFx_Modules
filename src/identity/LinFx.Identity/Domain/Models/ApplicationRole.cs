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

        /// <summary>
        /// A default role is automatically assigned to a new user
        /// </summary>
        public virtual bool IsDefault { get; set; }

        /// <summary>
        /// A static role can not be deleted/renamed
        /// </summary>
        public virtual bool IsStatic { get; set; }
    }
}