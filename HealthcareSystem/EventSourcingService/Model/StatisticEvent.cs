using EventSourcingService.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Model
{
    public class StatisticEvent : DomainEvent
    {
        public int SessionId { get; }
        public int UserAge { get; }
        public Gender UserGender { get; }
        public EventStep EventStep { get; }
        public ClickEvent ClickEvent { get; }

        public StatisticEvent(DateTime triggerTime, int sessionId, int userAge, Gender userGender, EventStep eventStep, ClickEvent clickEvent)
        {
            TriggerTime = triggerTime;
            SessionId = sessionId;
            UserAge = userAge;
            UserGender = userGender;
            EventStep = eventStep;
            ClickEvent = clickEvent;
        }
    }
}
