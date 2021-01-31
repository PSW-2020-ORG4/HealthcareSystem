using Backend.Model.Enums;
using Model.Manager;
using System.Collections.Generic;

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
