using MediatR;
using Ordering.Domain.Commands;
using Ordering.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Domain.Commands
{
    // Regular CommandHandler
    public class SetPaidOrderStatusCommandHandler : IRequestHandler<SetPaidOrderStatusCommand, bool>
    {        
        private readonly IOrderRepository _orderRepository;

        public SetPaidOrderStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Handler which processes the command when
        /// Shipment service confirms the payment
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<bool> Handle(SetPaidOrderStatusCommand command, CancellationToken cancellationToken)
        {
            // Simulate a work time for validating the payment
            await Task.Delay(10000);

            var orderToUpdate = await _orderRepository.GetAsync(command.OrderNumber);
            if(orderToUpdate == null)
            {
                return false;
            }

            orderToUpdate.SetPaidStatus();
            return await _orderRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }


    // Use for Idempotency in Command process
    public class SetPaidIdentifiedOrderStatusCommandHandler : IdentifiedCommandHandler<SetPaidOrderStatusCommand, bool>
    {
        public SetPaidIdentifiedOrderStatusCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for processing order.
        }
    }
}
