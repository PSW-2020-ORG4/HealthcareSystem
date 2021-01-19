using EventSourcingService.DTO;
using EventSourcingService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.Service
{
    public interface IEventStorePatientEndSchedulingService
    {
        public IEnumerable<PatientEndSchedulingEvent> GetAll();

        public PatientEndSchedulingEvent Add(PatientEndSchedulingEventDTO endSchedulingEventDTO);
    }
}
