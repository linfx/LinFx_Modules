using LinFx.Extensions.EventBus;
using LinFx.Extensions.EventBus;
using System;
using System.Threading.Tasks;

namespace Ordering.API.Application.IntegrationEvents
{
    public class OrderingIntegrationEventService : IOrderingIntegrationEventService
    {
        //private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        //private readonly OrderingContext _orderingContext;
        //private readonly IntegrationEventLogContext _eventLogContext;
        //private readonly IIntegrationEventLogService _eventLogService;

        public OrderingIntegrationEventService(IEventBus eventBus
            //OrderingContext orderingContext,
            //IntegrationEventLogContext eventLogContext,
            //Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory
            )
        {
            //_orderingContext = orderingContext ?? throw new ArgumentNullException(nameof(orderingContext));
            //_eventLogContext = eventLogContext ?? throw new ArgumentNullException(nameof(eventLogContext));
            //_integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            //_eventLogService = _integrationEventLogServiceFactory(_orderingContext.Database.GetDbConnection());
        }

        public Task PublishEventsThroughEventBusAsync()
        {
            //var pendindLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync();
            //foreach (var logEvt in pendindLogEvents)
            //{
            //    try
            //    {
            //        //await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
            //        _eventBus.PublishAsync(logEvt.IntegrationEvent);
            //        //await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
            //    }
            //    catch (Exception)
            //    {
            //        //await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
            //    }
            //}
            return Task.CompletedTask;
        }

        public Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            //await _eventLogService.SaveEventAsync(evt, _orderingContext.GetCurrentTransaction.GetDbTransaction());
            return Task.CompletedTask;
        }
    }
}
