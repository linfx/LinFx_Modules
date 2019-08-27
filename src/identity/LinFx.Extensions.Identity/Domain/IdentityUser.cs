using LinFx.Domain.Models;
using LinFx.Extensions.Auditing;
using LinFx.Extensions.MultiTenancy;
using System;
using System.ComponentModel.DataAnnotations;

namespace LinFx.Extensions.Identity.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    public class IdentityUser : Microsoft.AspNetCore.Identity.IdentityUser, IEntity<string>, IFullAuditedObject, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public string TenantId { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// 最后修改者
        /// </summary>
        public string LastModifierId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreationTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTimeOffset? LastModificationTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string DeleterId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTimeOffset? DeletionTime { get; set; }
    }
}
