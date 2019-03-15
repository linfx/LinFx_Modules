using LinFx.Extensions.Auditing;
using System;

namespace LinFx.Blogging.Domain.Models
{
    /// <summary>
    /// 评论
    /// </summary>
    public class Comment : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid PostId { get; protected set; }

        public virtual Guid? RepliedCommentId { get; protected set; }

        public virtual string Text { get; protected set; }
    }
}
