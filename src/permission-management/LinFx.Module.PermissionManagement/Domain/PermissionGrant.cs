using LinFx.Domain.Models;
using LinFx.Extensions.MultiTenancy;
using System.ComponentModel.DataAnnotations;

namespace LinFx.Extensions.PermissionManagement.Domain
{
    public class PermissionGrant : Entity<string>, IMultiTenant
    {
        [StringLength(36)]
        public virtual string TenantId { get; protected set; }

        [Required]
        [StringLength(64)]
        public virtual string Name { get; protected set; }

        [Required]
        [StringLength(64)]
        public virtual string ProviderName { get; protected set; }

        [StringLength(64)]
        public virtual string ProviderKey { get; protected set; }

        protected PermissionGrant() { }

        public PermissionGrant(string id, string name, string providerName, string providerKey, string tenantId = default)
        {
            Check.NotNull(name, nameof(name));

            Id = id;
            Name = name;
            ProviderName = providerName;
            ProviderKey = providerKey;
            TenantId = tenantId;
        }
    }
}