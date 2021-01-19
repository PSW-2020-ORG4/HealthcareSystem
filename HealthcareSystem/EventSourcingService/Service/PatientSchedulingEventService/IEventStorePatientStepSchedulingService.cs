using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EventSourcingService.DTO;
using EventSourcingService.Model;

namespace EventSourcingService.Service
{
    public interface IEventStorePatientStepSchedulingService
    {
        IEnumerable<PatientStepSchedulingEvent> GetAll();
        PatientStepSchedulingEvent Add(PatientStepSchedulingEventDTO schedulingEventDTO);
    }
}
