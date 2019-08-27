using System;

namespace LinFx.Extensions.Identity.Application.Models
{
    public class IdentityUserDto
    {   
        /// <summary>
        /// 租户ID
        /// </summary>
        public virtual string TenantId { set; get; }

        /// <summary>
        /// ID
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// 手机认证
        /// </summary>
        public virtual bool PhoneNumberConfirmed { get; set; }

        public virtual DateTimeOffset? LockoutEnd { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual int AccessFailedCount { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTimeOffset CreationTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTimeOffset? LastModificationTime { get; set; }
    }
}
