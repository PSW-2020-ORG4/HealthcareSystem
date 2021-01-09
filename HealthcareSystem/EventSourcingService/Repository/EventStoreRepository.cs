using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingService.Model;
using Microsoft.EntityFrameworkCore;

namespace EventSourcingService.Repository
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventSourcingDbContext _context;
        public EventStoreRepository(EventSourcingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DomainEvent> GetAllEvents()
        {
           return _context.CustomEvents.ToList();
        }

        public void Add(DomainEvent domainEvent)
        {
            _context.CustomEvents.Add(domainEvent);
            _context.SaveChanges();
        }
    }
}
