using System.Collections.Generic;
using EventSourcingService.Model;

namespace EventSourcingService.Service
{
    public interface IEventStoreService
    {
        public IEnumerable<DomainEvent> GetAllEvents();

        public void Add(DomainEvent domainEvent);
    }
}
