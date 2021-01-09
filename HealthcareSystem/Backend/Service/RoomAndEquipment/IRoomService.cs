using Backend.Model.Enums;
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
        ICollection<Room> GetRoomsByUsageAndEquipment(TypeOfUsage usage, ICollection<int> equipmentTypeIds);
        bool CheckIfRoomHasRequiredEquipment(int roomId, ICollection<int> requiredEquipmentTypeIds);
        bool CheckIfRoomExists(int roomId);

    }
}
