using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingService.DTO
{
    public class MinAvgMaxStatisticDTO
    {
        public int Minimum { get; set; }
        public int Average { get; set; }
        public int Maximum { get; set; }
    }
}
