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
        public DateTime StartSchedulingTime { get; }
        public ReasonForEndOfAppointment ReasonForEndOfAppointment { get; }

        public PatientEndSchedulingEvent(DateTime startSchedulingTime, ReasonForEndOfAppointment reason)
        {
            StartSchedulingTime = startSchedulingTime;
            ReasonForEndOfAppointment = reason;
        }
    }
}
