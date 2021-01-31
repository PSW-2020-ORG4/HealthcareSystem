using EventSourcingService.Model.Enum;
using System;

namespace EventSourcingService.DTO
{
    public class PatientStepSchedulingEventDTO
    {
        public DateTime TriggerTime { get; set; }
        public int StartSchedulingEventId { get; set; }
        public int UserAge { get; set; }
        public Gender UserGender { get; set; }
        public EventStep EventStep { get; set; }
        public ClickEvent ClickEvent { get; set; }
    }
}
