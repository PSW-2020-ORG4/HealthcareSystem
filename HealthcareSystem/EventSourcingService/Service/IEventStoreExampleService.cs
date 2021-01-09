using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EventSourcingService.Model;

namespace EventSourcingService.Service
{
    public interface IEventStoreExampleService
    {
        public IEnumerable<ExampleEvent> GetAll();

        public IEnumerable<ExampleEvent> GetAll(Expression<Func<ExampleEvent, bool>> condition);

        public void Add(ExampleEvent domainEvent);
    }
}
