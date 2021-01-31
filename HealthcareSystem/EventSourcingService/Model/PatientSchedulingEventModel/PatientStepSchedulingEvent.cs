using EventSourcingService.Model.Enum;

namespace EventSourcingService.Model
{
    public class PatientStepSchedulingEvent : DomainEvent
    {
        public int StartSchedulingEventId { get; set; }
        public int UserAge { get; set; }
        public Gender UserGender { get; set; }
        public EventStep EventStep { get; set; }
        public ClickEvent ClickEvent { get; set; }

    }
}
