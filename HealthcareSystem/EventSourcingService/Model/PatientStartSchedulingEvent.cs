using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Model
{
    public class PatientStartSchedulingEvent : DomainEvent
    {
        public PatientStartSchedulingEvent(DateTime triggerTime)
        {
            TriggerTime = triggerTime;
        }
    }
}
