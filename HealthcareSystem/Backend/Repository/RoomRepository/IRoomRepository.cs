using Backend.Model.Enums;
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
        void UpdateRoom(Room room);
        void DeleteRoom(int number);
        Room AddRoom(Room room);
        bool CheckIfRoomExists(int roomId);
    }
}
