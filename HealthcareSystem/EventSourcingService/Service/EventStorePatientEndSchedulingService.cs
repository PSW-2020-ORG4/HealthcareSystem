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
        public void Add(PatientEndSchedulingEventDTO endSchedulingEventDTO)
        {
            _patientEndSchedulingEventRepository.Add(new PatientEndSchedulingEvent(endSchedulingEventDTO.TriggerTime, endSchedulingEventDTO.StartSchedulingEventId));
        }

        public IEnumerable<PatientEndSchedulingEvent> GetAll()
        {
            return _patientEndSchedulingEventRepository.GetAll();
        }
    }
}
