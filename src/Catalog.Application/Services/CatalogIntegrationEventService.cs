using LinFx.Extensions.EventBus;
using System;
using System.Threading.Tasks;
using Catalog.Infrastructure;

namespace Catalog.Api.IntegrationEvents
{
    public class CatalogIntegrationEventService : ICatalogIntegrationEventService
    {
        //private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly CatalogContext _catalogContext;
        //private readonly IIntegrationEventLogService _eventLogService;

        public CatalogIntegrationEventService(
            IEventBus eventBus, 
            CatalogContext catalogContext
            /*Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory*/)
        {
            _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
            //_integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            //_eventLogService = _integrationEventLogServiceFactory(_catalogContext.Database.GetDbConnection());
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            try
            {
                //await _eventLogService.MarkEventAsInProgressAsync(evt.Id);
                await _eventBus.PublishAsync(evt);
                //await _eventLogService.MarkEventAsPublishedAsync(evt.Id);
            }
            catch (Exception)
            {
                //await _eventLogService.MarkEventAsFailedAsync(evt.Id);
            }
        }

        public Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent evt)
        {
            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            //await ResilientTransaction.New(_catalogContext)
            //    .ExecuteAsync(async () => {
            //        // Achieving atomicity between original catalog database operation and the IntegrationEventLog thanks to a local transaction
            //        await _catalogContext.SaveChangesAsync();
            //        await _eventLogService.SaveEventAsync(evt, _catalogContext.Database.CurrentTransaction.GetDbTransaction());
            //    });

            return Task.CompletedTask;
        }
    }
}
