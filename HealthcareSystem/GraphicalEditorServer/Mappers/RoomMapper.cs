using Backend.Model.Enums;
using GraphicalEditorServer.DTO;
using Model.Manager;

namespace GraphicalEditorServer.Mappers
{
    public class RoomMapper
    {

        public static RoomDTO BackendRoomToGraphicalEditorRoom(Room backendRoom)
        {
            RoomDTO roomDTO = new RoomDTO();
            roomDTO.Id = (long)backendRoom.Id;
            switch (backendRoom.Usage)
            {
                case TypeOfUsage.CONSULTING_ROOM:
                    roomDTO.Usage = TypeOfMapObject.EXAMINATION_ROOM;
                    break;
                case TypeOfUsage.SICKROOM:
                    roomDTO.Usage = TypeOfMapObject.HOSPITALIZATION_ROOM;
                    break;
                case TypeOfUsage.OPERATION_ROOM:
                    roomDTO.Usage = TypeOfMapObject.OPERATION_ROOM;
                    break;
                default:
                    break;
            }
            return roomDTO;
        }

        public static Room GraphicalEditorRoomToBackendRoom(RoomDTO room)
        {
            Room backendRoom = new Room();
            backendRoom.Id = (int)room.Id;
            switch (room.Usage)
            {
                case TypeOfMapObject.EXAMINATION_ROOM:
                    backendRoom.Usage = TypeOfUsage.CONSULTING_ROOM;
                    break;
                case TypeOfMapObject.HOSPITALIZATION_ROOM:
                    backendRoom.Usage = TypeOfUsage.SICKROOM;
                    break;
                case TypeOfMapObject.OPERATION_ROOM:
                    backendRoom.Usage = TypeOfUsage.OPERATION_ROOM;
                    break;
                default:
                    break;
            }
            return backendRoom;
        }


    }
}
