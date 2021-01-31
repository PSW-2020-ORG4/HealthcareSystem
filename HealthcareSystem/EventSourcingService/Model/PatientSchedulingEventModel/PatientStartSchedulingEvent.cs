using EventSourcingService.Model.Enum;

namespace EventSourcingService.Model
{
    public class PatientStartSchedulingEvent : DomainEvent
    {
        public int UserAge { get; set; }
        public Gender UserGender { get; set; }
    }
}
