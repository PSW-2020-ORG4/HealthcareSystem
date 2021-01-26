using Backend.Model.Manager;
using Backend.Model.PerformingExamination;
using GraphicalEditorServer.DTO;
using GraphicalEditorServer.Enumerations;

namespace GraphicalEditorServer.Mappers
{
    public class RoomSchedulesMapper
    {
        public static RoomSchedulesDTO Examination_To_RoomSchedulesDTO(Examination examination)
        {
            return new RoomSchedulesDTO(examination.DateAndTime, ScheduleType.Appointment, examination.Id);
        }

        public static RoomSchedulesDTO EquipmentTransfer_To_RoomSchedulesDTO(EquipmentTransfer equipmentTransfer)
        {
            return new RoomSchedulesDTO(equipmentTransfer.DateAndTimeOfTransfer, ScheduleType.EquipmentTransfer, equipmentTransfer.Id);
        }

        public static RoomSchedulesDTO Renovation_To_RoomSchedulesDTO(BaseRenovation renovation)
        {
            return new RoomSchedulesDTO(renovation.RenovationPeriod.BeginDate, ScheduleType.Renovation, renovation.Id);
        }
    }
}
