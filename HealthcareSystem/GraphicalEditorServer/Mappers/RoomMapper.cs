using GraphicalEditor.Enumerations;
using GraphicalEditor.Models.MapObjectRelated;
using GraphicalEditorServer.DTO;
using Model.Enums;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.Mappers
{
    public class RoomMapper
    {

        public static GraphicalEditor.Models.MapObjectRelated.Room BackendRoomToGraphicalEditorRoom(Model.Manager.Room beckendRoom)
        {
            Model.Manager.Room room = new Model.Manager.Room();

            // not implemented
            return null;
        }

        public static Model.Manager.Room GraphicalEditorRoomToBackendRoom(GraphicalEditor.Models.MapObjectRelated.Room room)
        {
            Model.Manager.Room backendRoom = new Model.Manager.Room();
            backendRoom.Id = (int) room.Id;
            TypeOfMapObject typeOfRoom = room.MapObjectType.TypeOfMapObject;

            switch (typeOfRoom)
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
