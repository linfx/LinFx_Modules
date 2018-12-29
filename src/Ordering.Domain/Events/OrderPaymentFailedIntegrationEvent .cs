namespace Ordering.API.Application.IntegrationEvents.Events
{
    using LinFx.Extensions.EventBus.Events;

    public class OrderPaymentFailedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public OrderPaymentFailedIntegrationEvent(int orderId) => OrderId = orderId;
    }
}