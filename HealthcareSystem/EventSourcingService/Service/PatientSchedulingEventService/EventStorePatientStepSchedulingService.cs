using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventSourcingService.DTO;
using EventSourcingService.Model;
using EventSourcingService.Repository;

namespace EventSourcingService.Service
{
    public class EventStorePatientStepSchedulingService : IEventStorePatientStepSchedulingService
    {

        private readonly IDomainEventRepository<PatientStepSchedulingEvent> _patientSchedulingEventRepository;

        public EventStorePatientStepSchedulingService(IDomainEventRepository<PatientStepSchedulingEvent> patientSchedulingEventRepository)
        {
            _patientSchedulingEventRepository = patientSchedulingEventRepository;
        }

        public IEnumerable<PatientStepSchedulingEvent> GetAll()
        {
            return _patientSchedulingEventRepository.GetAll();
        }

        public PatientStepSchedulingEvent Add(PatientStepSchedulingEventDTO patientSchedulingEventDTO)
        {
            return _patientSchedulingEventRepository.Add(new PatientStepSchedulingEvent()
            {
                StartSchedulingEventId = patientSchedulingEventDTO.PatientStartSchedulingEventId,
                UserAge = patientSchedulingEventDTO.UserAge, 
                UserGender = patientSchedulingEventDTO.UserGender,
                EventStep = patientSchedulingEventDTO.EventStep, 
                ClickEvent = patientSchedulingEventDTO.ClickEvent
            });
        }

    }
}
