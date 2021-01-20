using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingService.DTO.PatientSchedulingEventDTO;

namespace EventSourcingService.DTO
{
    public class PatientSchedulingStatisticDTO
    {
        public MinAvgMaxStatisticDTO successfulSchedulingDuration { get; set; }
        public GenderStatisticDTO successfulSchedulingGenderStatistic { get; set; }
        public MinAvgMaxStatisticDTO successfulSchedulingAgeStatistic { get; set; }
        public StepClosureStatisticDTO closedSchedulingStepStatistic { get; set; }
        public StepPreviousStatisticDTO previousSchedulingStepStatistic { get; set; }
    }
}
