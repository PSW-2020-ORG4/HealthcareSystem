using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.RoomAndEquipment
{
    public interface IEquipmentInRoomsService
    {
        EquipmentInRooms addEquipmentInRoom(EquipmentInRooms equipment);
        EquipmentInRooms editEquipmentInRooms(EquipmentInRooms equipment);
        List<Equipment> getEquipmentByRoomNumber(int roomNumber);
        bool deleteEquipmentInRooms(int idEquipment);
        int viewEquipmentInRooms(int idEquipment);
    }
}
