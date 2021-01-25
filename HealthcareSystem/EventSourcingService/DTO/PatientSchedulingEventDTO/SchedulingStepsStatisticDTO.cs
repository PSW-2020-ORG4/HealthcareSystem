using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.DTO.PatientSchedulingEventDTO
{
    public class SchedulingStepsStatisticDTO
   {
        public int NumberOfClosedScheduling { get; set; }
        public int NumberOfNextSteps { get; set; }
        public int NumberOfPreviousSteps { get; set; }
        public int NumberOfSteps { get; set; }
    }
}
