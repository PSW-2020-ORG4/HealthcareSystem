using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.DTO
{
    public class RenovationPeriodDTO
    {
       public DateTime StartTime { get; set; }
       public DateTime EndTime { get; set; }

        public RenovationPeriodDTO()
        {
        }

        public RenovationPeriodDTO(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
