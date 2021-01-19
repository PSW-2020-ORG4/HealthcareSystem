using EventSourcingService.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.DTO
{
    public class PatientEndSchedulingEventDTO
    {
        public DateTime TriggerTime { get; set; }
        public DateTime StartSchedulingTime { get; set; }
        public ReasonForEndOfAppointment ReasonForEndOfAppointment { get; set; }
    }
}
