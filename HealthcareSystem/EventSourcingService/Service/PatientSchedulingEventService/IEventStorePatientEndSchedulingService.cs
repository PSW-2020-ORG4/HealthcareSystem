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
        IEnumerable<PatientEndSchedulingEvent> GetAll();

        PatientEndSchedulingEvent Add(PatientEndSchedulingEventDTO endSchedulingEventDTO);

        MinAvgMaxStatisticDTO SuccessfulSchedulingDuration();
    }
}
