using MediatR;
using Ordering.Domain.Commands;
using Ordering.Domain.Events;
using System.Threading.Tasks;
using Xunit;

namespace Ordering.UnitTests
{
    public class OrderCommandTest
    {
        [Fact]
        public async Task Create_Order_Command()
        {
            IMediator _mediator = null;

            UserCheckoutAcceptedIntegrationEvent eventMsg = null;

            var createOrderCommand = new CreateOrderCommand(eventMsg.Basket.Items, eventMsg.UserId, eventMsg.UserName, eventMsg.City, eventMsg.Street,
                eventMsg.State, eventMsg.Country, eventMsg.ZipCode,
                eventMsg.CardNumber, eventMsg.CardHolderName, eventMsg.CardExpiration,
                eventMsg.CardSecurityNumber, eventMsg.CardTypeId);

            await _mediator.Send(createOrderCommand);
        }
    }
}
