using LinFx.Domain.Models;
using LinFx.Extensions.Auditing;
using LinFx.Extensions.MultiTenancy;
using LinFx.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace LinFx.Extensions.Identity.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    public class IdentityUser : IdentityUser<string>
    {
        public IdentityUser()
        {
            Id = IDUtils.NewId().ToString();
            SecurityStamp = Guid.NewGuid().ToString();
        }
    }

    /// <summary>
    /// 用户
    /// </summary>
    /// <typeparam name="TKey">The type used from the primary key for the user.</typeparam>
    public class IdentityUser<TKey> : Microsoft.AspNetCore.Identity.IdentityUser<TKey>, IEntity<TKey>, IFullAuditedObject, IMultiTenant
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        [StringLength(50)]
        public string TenantId { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [StringLength(50)]
        public string CreatorId { get; set; }

        /// <summary>
        /// 最后修改者
        /// </summary>
        [StringLength(50)]
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
        [StringLength(50)]
        public string DeleterId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTimeOffset? DeletionTime { get; set; }
    }
}
