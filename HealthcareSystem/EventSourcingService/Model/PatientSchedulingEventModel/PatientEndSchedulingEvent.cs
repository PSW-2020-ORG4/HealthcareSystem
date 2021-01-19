using EventSourcingService.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
