using EventSourcingService.Model.Enum;
using System;

namespace EventSourcingService.DTO
{
    public class PatientStartSchedulingEventDTO
    {
        public int Id { get; set; }
        public DateTime TriggerTime { get; set; }
        public int UserAge { get; set; }
        public Gender UserGender { get; set; }
    }
}
