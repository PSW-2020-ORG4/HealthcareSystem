using EventSourcingService.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Model
{
    public class PatientSchedulingEvent : DomainEvent
    {
        [ForeignKey("StartSchedulingEvent")]
        public int StartSchedulingEventId { get; }
        public virtual PatientStartSchedulingEvent StartSchedulingEvent { get; }
        public int UserAge { get; }
        public Gender UserGender { get; }
        public EventStep EventStep { get; }
        public ClickEvent ClickEvent { get; }

        public PatientSchedulingEvent(DateTime triggerTime, int startSchedulingEventId, int userAge, Gender userGender, EventStep eventStep, ClickEvent clickEvent)
        {
            TriggerTime = triggerTime;
            StartSchedulingEventId = startSchedulingEventId;
            UserAge = userAge;
            UserGender = userGender;
            EventStep = eventStep;
            ClickEvent = clickEvent;
        }
    }
}
