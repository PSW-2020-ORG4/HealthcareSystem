﻿using EventSourcingService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EventSourcingService.Repository
{
    public class DomainEventRepository<T> : IDomainEventRepository<T>
        where T : DomainEvent
    {
        private readonly EventSourcingDbContext _context;

        public DomainEventRepository(EventSourcingDbContext context)
        {
            _context = context;
        }

        public T Add(T domainEvent)
        {
            domainEvent.Id = 0;     // DB choose about ID
            domainEvent.TriggerTime = DateTime.Now;
            Set().Add(domainEvent);
            SaveChanges();

            return domainEvent;
        }

        public IEnumerable<T> GetAll()
        {
            return Set().ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> condition)
        {
            return Set().Where(condition).ToList();
        }

        private DbSet<T> Set()
            => _context.Set<T>();

        private void SaveChanges()
            => _context.SaveChanges();
    }
}
