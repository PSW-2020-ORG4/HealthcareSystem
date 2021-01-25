﻿using GraphicalEditorServer.Enumerations;
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

        public int ScheduleId { get; set; }

        public RoomSchedulesDTO() { }

        public RoomSchedulesDTO(DateTime scheduleTime, ScheduleType scheduleType, int scheduleId)
        {
            ScheduleTime = scheduleTime;
            ScheduleType = scheduleType;
            ScheduleId = scheduleId;
        }
    }
}
