using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingService.Model;

namespace EventSourcingService.Repository
{
    public interface IEventStoreRepository
    {
        IEnumerable<DomainEvent> GetAllEvents();

        void Add(DomainEvent domainEvent);
    }
}
