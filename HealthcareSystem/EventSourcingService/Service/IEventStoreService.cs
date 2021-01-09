using System.Collections.Generic;
using EventSourcingService.Model;

namespace EventSourcingService.Service
{
    public interface IEventStoreService
    {
        public IEnumerable<CustomEvent> GetAllEvents();

        public void Add(CustomEvent customEvent);
    }
}
