using MediatR;
using Ordering.Domain.Commands;
using Ordering.Domain.Interfaces;
using Ordering.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Domain.Commands
{
    // Regular CommandHandler
    public class SetStockRejectedOrderStatusCommandHandler : IRequestHandler<SetStockRejectedOrderStatusCommand, bool>
    {        
        private readonly IOrderRepository _orderRepository;

        public SetStockRejectedOrderStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Handler which processes the command when
        /// Stock service rejects the request
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<bool> Handle(SetStockRejectedOrderStatusCommand command, CancellationToken cancellationToken)
        {
            // Simulate a work time for rejecting the stock
            await Task.Delay(10000);

            var orderToUpdate = await _orderRepository.GetAsync(command.OrderNumber);
            if(orderToUpdate == null)
            {
                return false;
            }            

            orderToUpdate.SetCancelledStatusWhenStockIsRejected(command.OrderStockItems);

            return await _orderRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }


    // Use for Idempotency in Command process
    public class SetStockRejectedOrderStatusIdenfifiedCommandHandler : IdentifiedCommandHandler<SetStockRejectedOrderStatusCommand, bool>
    {
        public SetStockRejectedOrderStatusIdenfifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for processing order.
        }
    }
}
