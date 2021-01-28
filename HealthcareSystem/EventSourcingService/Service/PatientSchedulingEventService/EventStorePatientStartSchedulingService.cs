using EventSourcingService.DTO;
using EventSourcingService.Model;
using EventSourcingService.Repository;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingService.Service
{
    public class EventStorePatientStartSchedulingService : IEventStorePatientStartSchedulingService
    {
        private readonly IDomainEventRepository<PatientStartSchedulingEvent> _patientStartSchedulingEventRepository;

        public EventStorePatientStartSchedulingService(IDomainEventRepository<PatientStartSchedulingEvent> patientStartSchedulingEventRepository)
        {
            _patientStartSchedulingEventRepository = patientStartSchedulingEventRepository;
        }
        public PatientStartSchedulingEvent Add(PatientStartSchedulingEventDTO patientStartSchedulingEventDTO)
        {
            return _patientStartSchedulingEventRepository.Add(new PatientStartSchedulingEvent()
            {
                UserAge = patientStartSchedulingEventDTO.UserAge,
                UserGender = patientStartSchedulingEventDTO.UserGender
            });
        }

        public bool Contain(int id)
        {
            return _patientStartSchedulingEventRepository.GetAll().Where(e => e.Id == id).Any();
        }

        public IEnumerable<PatientStartSchedulingEvent> GetAll()
        {
            return _patientStartSchedulingEventRepository.GetAll();
        }
    }
}
