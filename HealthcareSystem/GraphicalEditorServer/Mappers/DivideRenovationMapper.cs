using Backend.Model.Enums;
using Backend.Model.Manager;
using GraphicalEditorServer.DTO;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.Mappers
{
    public class DivideRenovationMapper
    {
        public static DivideRenovation DivideRenovationDTOToDivideRenovation(DivideRenovationDTO divideRenovationDTO)
        {
            TypeOfUsage firstRoomType = new TypeOfUsage();
            TypeOfUsage secondRoomType = new TypeOfUsage();

            switch (divideRenovationDTO.FirstRoomType)
            {
                case TypeOfMapObject.EXAMINATION_ROOM:
                    firstRoomType = TypeOfUsage.CONSULTING_ROOM;
                    break;
                case TypeOfMapObject.HOSPITALIZATION_ROOM:
                    firstRoomType = TypeOfUsage.SICKROOM;
                    break;
                case TypeOfMapObject.OPERATION_ROOM:
                    firstRoomType = TypeOfUsage.OPERATION_ROOM;
                    break;
                default:
                    break;
            }

            switch (divideRenovationDTO.SecondRoomType)
            {
                case TypeOfMapObject.EXAMINATION_ROOM:
                    secondRoomType = TypeOfUsage.CONSULTING_ROOM;
                    break;
                case TypeOfMapObject.HOSPITALIZATION_ROOM:
                    secondRoomType = TypeOfUsage.SICKROOM;
                    break;
                case TypeOfMapObject.OPERATION_ROOM:
                    secondRoomType = TypeOfUsage.OPERATION_ROOM;
                    break;
                default:
                    break;
            }
            return new DivideRenovation(new RenovationPeriod(divideRenovationDTO.baseRenovation.StartTime, divideRenovationDTO.baseRenovation.EndTime),
                                                                  divideRenovationDTO.baseRenovation.Description,
                                                                  divideRenovationDTO.baseRenovation.TypeOfRenovation,
                                                                  divideRenovationDTO.baseRenovation.RoomId,
                                                                  divideRenovationDTO.FirstRoomDescription,
                                                                  divideRenovationDTO.SecondRoomDescription,
                                                                  firstRoomType,
                                                                  secondRoomType);           
                                                                  

        }
    }
}
