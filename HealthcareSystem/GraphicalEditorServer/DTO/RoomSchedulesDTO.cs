using GraphicalEditorServer.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.DTO
{
    public class RoomSchedulesDTO
    {
        public DateTime ScheduleTime { get; set; }
        public ScheduleType ScheduleType { get; set; }
        public int Id { get; set; }

        public RoomSchedulesDTO() { }

        public RoomSchedulesDTO(DateTime scheduleTime, ScheduleType scheduleType, int id)
        {
            ScheduleTime = scheduleTime;
            ScheduleType = scheduleType;
            Id = id;
        }
    }
}
