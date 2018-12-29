namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    using System.Threading.Tasks;
    using Events;
    using System.Linq;
    using MediatR;
    using Ordering.Domain.Commands;
    using LinFx.Extensions.EventBus.Abstractions;

    /// <summary>
    /// 订单库存调整
    /// </summary>
    public class OrderStockRejectedIntegrationEventHandler : IIntegrationEventHandler<OrderStockRejectedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public OrderStockRejectedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(OrderStockRejectedIntegrationEvent @event)
        {
            var orderStockRejectedItems = @event.OrderStockItems
                .FindAll(c => !c.HasStock)
                .Select(c => c.ProductId)
                .ToList();

            var command = new SetStockRejectedOrderStatusCommand(@event.OrderId, orderStockRejectedItems);
            await _mediator.Send(command);
        }
    }
}