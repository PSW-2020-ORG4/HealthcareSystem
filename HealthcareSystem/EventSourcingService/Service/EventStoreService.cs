using System.Collections.Generic;
using EventSourcingService.Model;
using EventSourcingService.Repository;

namespace EventSourcingService.Service
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public EventStoreService(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<DomainEvent> GetAllEvents()
        {
            return _eventStoreRepository.GetAllEvents();
        }

        public void Add(DomainEvent domainEvent)
        {
            _eventStoreRepository.Add(domainEvent);
        }
    }
}
