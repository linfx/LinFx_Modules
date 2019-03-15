using LinFx.Extensions.Auditing;
using System;

namespace LinFx.Blogging.Domain.Models
{
    /// <summary>
    /// 标签
    /// </summary>
    public class Tag : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid BlogId { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual string Description { get; protected set; }

        public virtual int UsageCount { get; protected internal set; }
    }
}
