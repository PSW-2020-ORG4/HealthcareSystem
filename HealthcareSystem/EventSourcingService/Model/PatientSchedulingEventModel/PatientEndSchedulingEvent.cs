using EventSourcingService.Model.Enum;
using System;

namespace EventSourcingService.Model
{
    public class PatientEndSchedulingEvent : DomainEvent
    {
        public DateTime StartSchedulingEventTime { get; set; }
        public int UserAge { get; set; }
        public Gender UserGender { get; set; }
        public ReasonForEndOfAppointment ReasonForEndOfAppointment { get; set; }

    }
}
