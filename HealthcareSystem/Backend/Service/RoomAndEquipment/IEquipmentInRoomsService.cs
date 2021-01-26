using Model.Manager;
using System.Collections.Generic;

namespace Backend.Service.RoomAndEquipment
{
    public interface IEquipmentInRoomsService
    {
        EquipmentInRooms AddEquipmentInRoom(EquipmentInRooms equipment);
        EquipmentInRooms EditEquipmentInRooms(EquipmentInRooms equipment);
        void DeleteEquipmentInRooms(int idEquipment);
        List<EquipmentInRooms> GetEquipmentInRoomsFromEquipment(Equipment equipment);
    }
}
