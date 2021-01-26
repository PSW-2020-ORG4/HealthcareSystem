using EventSourcingService.DTO;
using EventSourcingService.Model;
using System.Collections.Generic;

namespace EventSourcingService.Service
{
    public interface IEventStorePatientEndSchedulingService
    {
        IEnumerable<PatientEndSchedulingEvent> GetAll();
        PatientEndSchedulingEvent Add(PatientEndSchedulingEventDTO endSchedulingEventDTO);
        MinAvgMaxStatisticDTO SuccessfulSchedulingDuration();
        GenderStatisticDTO SuccessfulSchedulingGenderStatistic();
        MinAvgMaxStatisticDTO SuccessfulSchedulingAgeStatistic();
        AverageDurationDTO SuccessfulSchedulingAgeDuration();
        AverageDurationDTO SuccessfulSchedulingGenderDuration();
        AverageDurationDTO UnsuccessfulSchedulingAgeDuration();
        AverageDurationDTO UnsuccessfulSchedulingGenderDuration();
        bool Contain(int id);
        SuccessfulAndUnsuccessfulSchedulingDTO SuccessfulAndUnsuccessfulScheduling();
    }
}
