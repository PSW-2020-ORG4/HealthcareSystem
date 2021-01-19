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
    public class EventStorePatientSchedulingService : IEventStorePatientSchedulingService
    {

        private readonly IDomainEventRepository<PatientSchedulingEvent> _patientSchedulingEventRepository;

        public EventStorePatientSchedulingService(IDomainEventRepository<PatientSchedulingEvent> patientSchedulingEventRepository)
        {
            _patientSchedulingEventRepository = patientSchedulingEventRepository;
        }

        public IEnumerable<PatientSchedulingEvent> GetAll()
        {
            return _patientSchedulingEventRepository.GetAll();
        }

        public void Add(PatientSchedulingEventDTO patientSchedulingEvent)
        {
            _patientSchedulingEventRepository.Add(new PatientSchedulingEvent(patientSchedulingEvent.PatientStartSchedulingEventId, 
                                        patientSchedulingEvent.UserAge, patientSchedulingEvent.UserGender, 
                                        patientSchedulingEvent.EventStep, patientSchedulingEvent.ClickEvent));
        }

    }
}
