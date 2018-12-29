namespace Ordering.API.Application.IntegrationEvents.Events
{
    using LinFx.Extensions.EventBus.Events;

    public class GracePeriodConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public GracePeriodConfirmedIntegrationEvent(int orderId) =>
            OrderId = orderId;
    }
}
