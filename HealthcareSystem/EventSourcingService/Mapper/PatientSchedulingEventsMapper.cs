using EventSourcingService.DTO;
using EventSourcingService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Mapper
{
    public static class PatientSchedulingEventsMapper
    {
        public static PatientStepSchedulingEventDTO ToPatientSchedulingEventDTO(this PatientStepSchedulingEvent patientSchedulingEvent)
        {
            PatientStepSchedulingEventDTO patientSchedulingDTO = new PatientStepSchedulingEventDTO()
            {
                TriggerTime = patientSchedulingEvent.TriggerTime,
                PatientStartSchedulingEventId = patientSchedulingEvent.StartSchedulingEventId,
                UserAge = patientSchedulingEvent.UserAge,
                UserGender = patientSchedulingEvent.UserGender,
                EventStep = patientSchedulingEvent.EventStep,
                ClickEvent = patientSchedulingEvent.ClickEvent
            };
            return patientSchedulingDTO;
        }

        public static PatientStartSchedulingEventDTO ToPatientStartSchedulingEventDTO(this PatientStartSchedulingEvent startSchedulingEvent)
        {
            PatientStartSchedulingEventDTO startSchedulingEventDTO = new PatientStartSchedulingEventDTO()
            {
                Id = startSchedulingEvent.Id,
                TriggerTime = startSchedulingEvent.TriggerTime
            };
            return startSchedulingEventDTO;
        }

        public static PatientEndSchedulingEventDTO ToPatientEndSchedulingEventDTO(this PatientEndSchedulingEvent endSchedulingEvent)
        {
            PatientEndSchedulingEventDTO endSchedulingEventDTO = new PatientEndSchedulingEventDTO()
            {
                TriggerTime = endSchedulingEvent.TriggerTime,
                StartSchedulingTime = endSchedulingEvent.StartSchedulingTime,
                ReasonForEndOfAppointment = endSchedulingEvent.ReasonForEndOfAppointment
            };
            return endSchedulingEventDTO;
        }
    }
}
