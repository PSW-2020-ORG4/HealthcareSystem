using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.DTO
{
    public class PatientSchedulingStatisticDTO
    {
        public MinAvgMaxStatisticDTO successfulSchedulingDuration { get; set; }
    }
}
