﻿using EventSourcingService.DTO.PatientSchedulingEventDTO;

namespace EventSourcingService.DTO
{
    public class PatientSchedulingStatisticDTO
    {
        public MinAvgMaxStatisticDTO successfulSchedulingDuration { get; set; }
        public GenderStatisticDTO successfulSchedulingGenderStatistic { get; set; }
        public MinAvgMaxStatisticDTO successfulSchedulingAgeStatistic { get; set; }
        public StepClosureStatisticDTO closedSchedulingStepStatistic { get; set; }
        public StepPreviousStatisticDTO previousSchedulingStepStatistic { get; set; }
        public AverageDurationDTO successfulSchedulingAgeDuration { get; set; }
        public AverageDurationDTO successfulSchedulingGenderDuration { get; set; }
        public AverageDurationDTO unsuccessfulSchedulingAgeDuration { get; set; }
        public AverageDurationDTO unsuccessfulSchedulingGenderDuration { get; set; }
        public SchedulingStepsStatisticDTO schedulingStepsStatistic { get; set; }
        public SuccessfulAndUnsuccessfulSchedulingDTO successfulAndUnsuccessfulSchedulingDTO { get; set; }
    }
}
