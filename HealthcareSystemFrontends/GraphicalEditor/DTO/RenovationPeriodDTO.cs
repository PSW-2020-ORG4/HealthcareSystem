using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.DTO
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
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

    }
}
