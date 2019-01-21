namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
   using LinFx.Extensions.EventBus;
    using MediatR;
    using Ordering.Domain.Commands;
    using Ordering.API.Application.IntegrationEvents.Events;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// 订单支付失败
    /// </summary>
    public class OrderPaymentFailedIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public OrderPaymentFailedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(OrderPaymentFailedIntegrationEvent @event)
        {
            var command = new CancelOrderCommand(@event.OrderId);
            await _mediator.Send(command);
        }
    }

    /// <summary>
    /// 订单支付成功
    /// </summary>
    public class OrderPaymentSuccededIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentSuccededIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public OrderPaymentSuccededIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(OrderPaymentSuccededIntegrationEvent @event)
        {
            var command = new SetPaidOrderStatusCommand(@event.OrderId);
            await _mediator.Send(command);
        }
    }
}
