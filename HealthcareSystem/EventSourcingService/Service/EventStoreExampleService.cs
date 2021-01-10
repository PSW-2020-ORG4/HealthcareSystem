using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventSourcingService.Model;
using EventSourcingService.Repository;

namespace EventSourcingService.Service
{
    public class EventStoreExampleService : IEventStoreExampleService
    {

        private readonly IDomainEventRepository<ExampleEvent> _exampleEventRepository;

        public EventStoreExampleService(IDomainEventRepository<ExampleEvent> exampleEventRepository)
        {
            _exampleEventRepository = exampleEventRepository;
        }

        public IEnumerable<ExampleEvent> GetAll()
        {
            return _exampleEventRepository.GetAll();
        }

        public void Add(ExampleEvent exampleEvent)
        {
            _exampleEventRepository.Add(exampleEvent);
        }

    }
}
