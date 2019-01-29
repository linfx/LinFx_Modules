using LinFx.Extensions.EventBus;
using LinFx.Extensions.EventStores;
using System;
using System.Threading.Tasks;
using Catalog.Infrastructure;

namespace Catalog.Application.Services
{
    public class CatalogIntegrationEventService : ICatalogIntegrationEventService
    {
        private readonly IEventBus _eventBus;
        private readonly IEventStore _eventStore;
        private readonly CatalogContext _catalogContext;

        public CatalogIntegrationEventService(
            IEventBus eventBus,
            IEventStore eventStore, 
            CatalogContext catalogContext)
        {
            _eventBus = eventBus;
            _eventStore = eventStore;
            _catalogContext = catalogContext;
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            try
            {
                await _eventStore.MarkEventAsInProgressAsync(evt.Id);
                //await _eventBus.PublishAsync(evt);
                await _eventStore.MarkEventAsPublishedAsync(evt.Id);
            }
            catch (Exception ex)
            {
                //await _eventLogService.MarkEventAsFailedAsync(evt.Id);
            }
        }

        public Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent evt)
        {
            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            return ResilientTransaction.New(_catalogContext).ExecuteAsync(async () =>
            {
                // Achieving atomicity between original catalog database operation and the IntegrationEventLog thanks to a local transaction
                await _catalogContext.SaveChangesAsync();
                //await _eventLogService.SaveEventAsync(evt, _catalogContext.Database.CurrentTransaction.GetDbTransaction());
            });
        }
    }
}