using GraphicalEditorServer.Enumerations;
using System;

namespace GraphicalEditorServer.DTO
{
    public class RoomSchedulesDTO
    {
        public DateTime ScheduleTime { get; set; }
        public ScheduleType ScheduleType { get; set; }

        public RoomSchedulesDTO() { }

        public RoomSchedulesDTO(DateTime scheduleTime, ScheduleType scheduleType)
        {
            ScheduleTime = scheduleTime;
            ScheduleType = scheduleType;
        }
    }
}
