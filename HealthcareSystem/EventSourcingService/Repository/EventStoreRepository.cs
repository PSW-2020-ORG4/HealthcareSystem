using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingService.Model;
using Microsoft.EntityFrameworkCore;

namespace EventSourcingService.Repository
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly CustomDbContext _context;
        public EventStoreRepository(CustomDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CustomEvent> GetAllEvents()
        {
           return _context.CustomEvents.ToList();
        }

        public void Add(CustomEvent customEvent)
        {
            _context.CustomEvents.Add(customEvent);
            _context.SaveChanges();
        }
    }
}
