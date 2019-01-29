using LinFx.Extensions.EventBus;
using System.Threading.Tasks;

namespace Catalog.Application.Services
{
    public interface ICatalogIntegrationEventService
    {
        Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
