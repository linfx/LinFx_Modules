namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    using System.Threading.Tasks;
    using Events;
    using MediatR;
    using System;
    using Ordering.Domain.Commands;
    using LinFx.Extensions.EventBus.Abstractions;
    using System.Linq;

    public class OrderStockConfirmedIntegrationEventHandler : IIntegrationEventHandler<OrderStockConfirmedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public OrderStockConfirmedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(OrderStockConfirmedIntegrationEvent @event)
        {
            var command = new SetStockConfirmedOrderStatusCommand(@event.OrderId);
            await _mediator.Send(command);
        }
    }

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