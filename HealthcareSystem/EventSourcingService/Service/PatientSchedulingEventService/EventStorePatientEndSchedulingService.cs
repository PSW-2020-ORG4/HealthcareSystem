using EventSourcingService.DTO;
using EventSourcingService.Model;
using EventSourcingService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Service
{
    public class EventStorePatientEndSchedulingService : IEventStorePatientEndSchedulingService
    {
        private readonly IDomainEventRepository<PatientEndSchedulingEvent> _patientEndSchedulingEventRepository;

        public EventStorePatientEndSchedulingService(IDomainEventRepository<PatientEndSchedulingEvent> patientEndSchedulingEventRepository)
        {
            _patientEndSchedulingEventRepository = patientEndSchedulingEventRepository;
        }
        public PatientEndSchedulingEvent Add(PatientEndSchedulingEventDTO endSchedulingEventDTO)
        {
            return _patientEndSchedulingEventRepository.Add(new PatientEndSchedulingEvent()
            {
                StartSchedulingTime = endSchedulingEventDTO.StartSchedulingTime,
                ReasonForEndOfAppointment = endSchedulingEventDTO.ReasonForEndOfAppointment
            });
        }

        public IEnumerable<PatientEndSchedulingEvent> GetAll()
        {
            return _patientEndSchedulingEventRepository.GetAll();
        }
    }
}
