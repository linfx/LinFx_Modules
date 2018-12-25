using LinFx.Extensions.EventBus.Events;
using System.Threading.Tasks;

namespace ShopFx.Catalog.Api.IntegrationEvents
{
    public interface ICatalogIntegrationEventService
    {
        Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
