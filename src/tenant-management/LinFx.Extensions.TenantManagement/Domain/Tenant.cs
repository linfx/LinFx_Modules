using LinFx.Domain.Models.Auditing;
using System.ComponentModel.DataAnnotations;

namespace LinFx.Extensions.TenantManagement.Domain
{
    /// <summary>
    /// 租户
    /// </summary>
    public class Tenant : FullAuditedAggregateRoot<string>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public virtual string Name { get; protected set; }
    }
}
