﻿using Backend.Model.Manager;
using Backend.Model.PerformingExamination;
using GraphicalEditorServer.DTO;
using GraphicalEditorServer.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.Mappers
{
    public class RoomSchedulesMapper
    {
        public static RoomSchedulesDTO Examination_To_RoomSchedulesDTO(Examination examination)
        {
            return new RoomSchedulesDTO(examination.DateAndTime, ScheduleType.Appointment);
        }

        public static RoomSchedulesDTO EquipmentTransfer_To_RoomSchedulesDTO(EquipmentTransfer equipmentTransfer)
        {
            return new RoomSchedulesDTO(equipmentTransfer.DateAndTimeOfTransfer, ScheduleType.EquipmentTransfer);
        }

        public static RoomSchedulesDTO Renovation_To_RoomSchedulesDTO(BaseRenovation renovation)
        {
            return new RoomSchedulesDTO(renovation.RenovationPeriod.BeginDate, ScheduleType.Renovation);
        }
    }
}