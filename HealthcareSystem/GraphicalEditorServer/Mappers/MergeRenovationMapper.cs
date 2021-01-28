using Backend.Model.Enums;
using Backend.Model.Manager;
using GraphicalEditorServer.DTO;
using Model.Manager;

namespace GraphicalEditorServer.Mappers
{
    public class MergeRenovationMapper
    {
        public static MergeRenovation MergeRenovationDTOToMergeRenovation(MergeRenovationDTO mergeRenovationDTO)
        {
            TypeOfUsage roomType = new TypeOfUsage();
            switch (mergeRenovationDTO.RoomType)
            {
                case TypeOfMapObject.EXAMINATION_ROOM:
                    roomType = TypeOfUsage.CONSULTING_ROOM;
                    break;
                case TypeOfMapObject.HOSPITALIZATION_ROOM:
                    roomType = TypeOfUsage.SICKROOM;
                    break;
                case TypeOfMapObject.OPERATION_ROOM:
                    roomType = TypeOfUsage.OPERATION_ROOM;
                    break;
                default:
                    break;
            }
            return new MergeRenovation(new RenovationPeriod(mergeRenovationDTO.baseRenovation.StartTime, mergeRenovationDTO.baseRenovation.EndTime),
                                                                  mergeRenovationDTO.baseRenovation.Description,
                                                                  mergeRenovationDTO.baseRenovation.TypeOfRenovation,
                                                                  mergeRenovationDTO.baseRenovation.RoomId,
                                                                  mergeRenovationDTO.SecondRoomId,
                                                                  mergeRenovationDTO.NewRoomDescription,
                                                                  roomType);

        }
    }
}
