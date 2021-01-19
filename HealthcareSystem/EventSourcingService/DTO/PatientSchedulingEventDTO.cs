using EventSourcingService.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.DTO
{
    public class PatientSchedulingEventDTO
    {
        public DateTime TriggerTime { get; set; }
        public int PatientStartSchedulingEventId { get; set; }
        public int UserAge { get; set; }
        public Gender UserGender { get; set; }
        public EventStep EventStep { get; set; }
        public ClickEvent ClickEvent { get; set; }
    }
}
