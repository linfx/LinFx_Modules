using LinFx.Domain.Models.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace LinFx.Identity.Domain.Models
{
    public class Tenant : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(200)]
        public string Name { get; protected set; }
    }
}
