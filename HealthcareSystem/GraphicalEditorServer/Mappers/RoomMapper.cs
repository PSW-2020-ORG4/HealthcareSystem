using Backend.Model.Enums;
using GraphicalEditorServer.DTO;
using Model.Manager;
using System;

namespace GraphicalEditorServer.Mappers
{
    public class RoomMapper
    {

        public static RoomDTO BackendRoomToGraphicalEditorRoom(Room beckendRoom)
        {
            throw new NotImplementedException();
        }

        public static Room GraphicalEditorRoomToBackendRoom(RoomDTO room)
        {
            Room backendRoom = new Room();
            backendRoom.Id = (int) room.Id;
            switch (room.Usage)
            {
                case TypeOfMapObject.EXAMINATION_ROOM :
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
