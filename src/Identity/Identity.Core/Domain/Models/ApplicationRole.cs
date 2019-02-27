using LinFx.Extensions.MultiTenancy;
using Microsoft.AspNetCore.Identity;
using System;

namespace Identity.Domain.Models
{
    /// <summary>
    /// Represents a role in the identity system
    /// </summary>
    public class ApplicationRole : IdentityRole, IMultiTenant
    {
        public Guid? TenantId { set; get; }

        public ApplicationRole() { }

        public ApplicationRole(string roleName) : base(roleName) { }
    }
}