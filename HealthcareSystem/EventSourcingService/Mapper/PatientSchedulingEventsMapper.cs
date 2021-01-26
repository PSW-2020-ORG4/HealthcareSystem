using EventSourcingService.DTO;
using EventSourcingService.Model;

namespace EventSourcingService.Mapper
{
    public static class PatientSchedulingEventsMapper
    {
        public static PatientStartSchedulingEventDTO ToPatientStartSchedulingEventDTO(this PatientStartSchedulingEvent startSchedulingEvent)
        {
            PatientStartSchedulingEventDTO startSchedulingEventDTO = new PatientStartSchedulingEventDTO()
            {
                Id = startSchedulingEvent.Id,
                TriggerTime = startSchedulingEvent.TriggerTime,
                UserAge = startSchedulingEvent.UserAge,
                UserGender = startSchedulingEvent.UserGender
            };
            return startSchedulingEventDTO;
        }
        public static PatientStepSchedulingEventDTO ToPatientStepSchedulingEventDTO(this PatientStepSchedulingEvent patientSchedulingEvent)
        {
            PatientStepSchedulingEventDTO patientSchedulingDTO = new PatientStepSchedulingEventDTO()
            {
                TriggerTime = patientSchedulingEvent.TriggerTime,
                StartSchedulingEventId = patientSchedulingEvent.StartSchedulingEventId,
                UserAge = patientSchedulingEvent.UserAge,
                UserGender = patientSchedulingEvent.UserGender,
                EventStep = patientSchedulingEvent.EventStep,
                ClickEvent = patientSchedulingEvent.ClickEvent
            };
            return patientSchedulingDTO;
        }

        public static PatientEndSchedulingEventDTO ToPatientEndSchedulingEventDTO(this PatientEndSchedulingEvent endSchedulingEvent)
        {
            PatientEndSchedulingEventDTO endSchedulingEventDTO = new PatientEndSchedulingEventDTO()
            {
                TriggerTime = endSchedulingEvent.TriggerTime,
                StartSchedulingEventTime = endSchedulingEvent.StartSchedulingEventTime,
                UserAge = endSchedulingEvent.UserAge,
                UserGender = endSchedulingEvent.UserGender,
                ReasonForEndOfAppointment = endSchedulingEvent.ReasonForEndOfAppointment
            };
            return endSchedulingEventDTO;
        }
    }
}
