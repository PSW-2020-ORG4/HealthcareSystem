using Model.Enums;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.RoomAndEquipment
{
    public interface IRoomService
    {
        int getLastId();
        void AddRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(int number);
        List<Room> ViewRooms();
        Room ViewRoomByNumber(int number);
        List<Room> ViewRoomByUsage(TypeOfUsage usage, DateTime beginDate, DateTime endDate);

    }
}
