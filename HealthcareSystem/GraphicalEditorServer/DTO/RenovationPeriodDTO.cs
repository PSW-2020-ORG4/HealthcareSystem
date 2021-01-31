using System;

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
