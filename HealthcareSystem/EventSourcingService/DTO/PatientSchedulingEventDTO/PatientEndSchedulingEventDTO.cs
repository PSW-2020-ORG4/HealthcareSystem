using EventSourcingService.Model.Enum;
using System;

namespace EventSourcingService.DTO
{
    public class PatientEndSchedulingEventDTO
    {
        public DateTime TriggerTime { get; set; }
        public int StartSchedulingEventId { get; set; }
        public DateTime StartSchedulingEventTime { get; set; }
        public int UserAge { get; set; }
        public Gender UserGender { get; set; }
        public ReasonForEndOfAppointment ReasonForEndOfAppointment { get; set; }
    }
}
