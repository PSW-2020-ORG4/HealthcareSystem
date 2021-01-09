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

        public IEnumerable<CustomEvent> GetAllEvents()
        {
            return _eventStoreRepository.GetAllEvents();
        }

        public void Add(CustomEvent customEvent)
        {
            _eventStoreRepository.Add(customEvent);
        }
    }
}
