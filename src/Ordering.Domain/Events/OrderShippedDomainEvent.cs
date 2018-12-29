using MediatR;
using Ordering.Domain.Models;

namespace Ordering.Domain.Events
{
    public class OrderShippedDomainEvent : INotification
    {
        public Order Order { get; }

        public OrderShippedDomainEvent(Order order)
        {
            Order = order;           
        }
    }
}
