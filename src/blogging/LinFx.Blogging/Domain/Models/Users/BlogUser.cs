using LinFx.Domain.Models;
using System;

namespace LinFx.Blogging.Domain.Models
{
    /// <summary>
    /// 博主
    /// </summary>
    public class BlogUser : AggregateRoot<Guid>
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual string UserName { get; protected set; }

        public virtual string Email { get; protected set; }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual bool EmailConfirmed { get; protected set; }

        public virtual string PhoneNumber { get; protected set; }

        public virtual bool PhoneNumberConfirmed { get; protected set; }
    }
}
