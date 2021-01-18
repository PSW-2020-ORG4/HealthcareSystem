using EventSourcingService.DTO;
using EventSourcingService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Mapper
{
    public static class EventMapper
    {
        public static StatisticEventDTO ToStatisticEventDTO(this StatisticEvent statisticEvent)
        {
            StatisticEventDTO statisticEventDTO = new StatisticEventDTO()
            {
                TriggerTime = statisticEvent.TriggerTime,
                SessionId = statisticEvent.SessionId,
                UserAge = statisticEvent.UserAge,
                UserGender = statisticEvent.UserGender,
                EventStep = statisticEvent.EventStep,
                ClickEvent = statisticEvent.ClickEvent
            };
            return statisticEventDTO;
        }
    }
}
