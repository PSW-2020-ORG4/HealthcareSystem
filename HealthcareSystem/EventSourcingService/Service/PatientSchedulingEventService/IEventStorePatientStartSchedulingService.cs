using EventSourcingService.DTO;
using EventSourcingService.Model;
using System.Collections.Generic;

namespace EventSourcingService.Service
{
    public interface IEventStorePatientStartSchedulingService
    {
        IEnumerable<PatientStartSchedulingEvent> GetAll();
        PatientStartSchedulingEvent Add(PatientStartSchedulingEventDTO patientStartSchedulingEventDTO);
        bool Contain(int id);
    }
}
