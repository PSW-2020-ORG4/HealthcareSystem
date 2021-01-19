using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Model
{
    public class PatientEndSchedulingEvent : DomainEvent
    {
        [ForeignKey("StartSchedulingEvent")]
        public int StartSchedulingEventId { get; }
        public virtual PatientStartSchedulingEvent StartSchedulingEvent { get; }

        public PatientEndSchedulingEvent(DateTime triggerTime, int startSchedulingEventId)
        {
            TriggerTime = triggerTime;
            StartSchedulingEventId = startSchedulingEventId;
        }
    }
}
