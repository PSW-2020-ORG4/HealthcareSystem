using Model.Enums;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.RoomRepository
{
    public interface IRoomRepository
    {
        int getLastId();
        Room GetRoomByNumber(int number);
        List<Room> GetRoomsByUsage(TypeOfUsage usage);
        List<Room> GetAllRooms();
        List<Room> GetRoomsByUsageAndEquipment(TypeOfUsage usage, ICollection<int> equipmentTypeIds);
        void UpdateRoom(Room room);
        void DeleteRoom(int number);
        void AddRoom(Room room);
        bool CheckIfRoomExists(int roomId);
    }
}
