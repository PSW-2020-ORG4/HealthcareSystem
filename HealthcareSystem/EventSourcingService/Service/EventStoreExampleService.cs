using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventSourcingService.DTO;
using EventSourcingService.Model;
using EventSourcingService.Repository;

namespace EventSourcingService.Service
{
    public class EventStoreExampleService : IEventStoreExampleService
    {

        private readonly IDomainEventRepository<StatisticEvent> _exampleEventRepository;

        public EventStoreExampleService(IDomainEventRepository<StatisticEvent> exampleEventRepository)
        {
            _exampleEventRepository = exampleEventRepository;
        }

        public IEnumerable<StatisticEvent> GetAll()
        {
            return _exampleEventRepository.GetAll();
        }

        public void Add(StatisticEventDTO statisticEventDTO)
        {
            _exampleEventRepository.Add(new StatisticEvent(statisticEventDTO.TriggerTime, statisticEventDTO.SessionId, 
                                        statisticEventDTO.UserAge, statisticEventDTO.UserGender, 
                                        statisticEventDTO.EventStep, statisticEventDTO.ClickEvent));
        }

    }
}
