using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.DTO
{
    public class PatientStartSchedulingEventDTO
    {
        public int Id { get; set; }
        public DateTime TriggerTime { get; set; }
    }
}
