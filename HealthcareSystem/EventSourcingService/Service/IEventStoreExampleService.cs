using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EventSourcingService.DTO;
using EventSourcingService.Model;

namespace EventSourcingService.Service
{
    public interface IEventStoreExampleService
    {
        public IEnumerable<StatisticEvent> GetAll();

        public void Add(StatisticEventDTO statisticEventDTO);
    }
}
