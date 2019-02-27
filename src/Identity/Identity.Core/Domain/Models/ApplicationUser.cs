using LinFx.Extensions.MultiTenancy;
using Microsoft.AspNetCore.Identity;
using System;

namespace Identity.Domain.Models
{
    /// <summary>
    /// Represents a user in the identity system
    /// </summary>
    public class ApplicationUser : IdentityUser, IMultiTenant
    {
        public Guid? TenantId { set; get; }
    }
}
