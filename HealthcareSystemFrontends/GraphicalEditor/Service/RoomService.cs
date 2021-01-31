using GraphicalEditor.Enumerations;
using GraphicalEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicalEditor.DTO;

namespace GraphicalEditor.Service
{
    public class RoomService : GenericHTTPService
    {
        public List<RoomDTO> GetAllRooms()
        {
            return (List<RoomDTO>)HTTPGetRequest<RoomDTO>("room");
        }

        public List<RoomSchedulesDTO> GetRoomSchedules(int roomId)
        {
            return (List<RoomSchedulesDTO>)HTTPGetRequest<RoomSchedulesDTO>("room/roomSchedule/" + roomId);
        }
    }
}
