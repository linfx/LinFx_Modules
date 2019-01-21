using LinFx.Extensions.EventBus;
using System.Threading.Tasks;

namespace Ordering.API.Application.IntegrationEvents
{
    public interface IOrderingIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync();
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
