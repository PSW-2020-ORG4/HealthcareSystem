using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EventSourcingService.DTO;
using EventSourcingService.Model;

namespace EventSourcingService.Service
{
    public interface IEventStorePatientSchedulingService
    {
        public IEnumerable<PatientSchedulingEvent> GetAll();

        public void Add(PatientSchedulingEventDTO schedulingEventDTO);
    }
}
