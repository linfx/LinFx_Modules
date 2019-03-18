﻿using LinFx.Extensions.Auditing;
using System;

namespace LinFx.Blogging.Domain.Models
{
    /// <summary>
    /// 帖子标签
    /// </summary>
    public class PostTag : CreationAuditedEntity
    {
        /// <summary>
        /// 博客ID
        /// </summary>
        public virtual Guid PostId { get; protected set; }

        /// <summary>
        /// 帖子ID
        /// </summary>
        public virtual Guid TagId { get; protected set; }

        /// <summary>
        /// 帖子标签
        /// </summary>
        /// <param name="postId">博客ID</param>
        /// <param name="tagId">帖子ID</param>
        public PostTag(Guid postId, Guid tagId)
        {
            PostId = postId;
            TagId = tagId;
        }

        public override object[] GetKeys()
        {
            return new object[] { PostId, TagId };
        }
    }
}