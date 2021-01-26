using Model.Manager;
using System.Collections.Generic;

namespace Backend.Repository.EquipmentInRoomsRepository
{
    public interface IEquipmentInRoomsRepository
    {
        EquipmentInRooms UpdateEquipment(EquipmentInRooms equipment);
        void DeleteEquipment(int id);
        EquipmentInRooms AddEquipment(EquipmentInRooms equipment);
        List<EquipmentInRooms> GetEquipmenInRoomsByEquipmentId(int equipmentId);
        List<EquipmentInRooms> GetEquipmentInRoomsByRoomNumber(int roomNumber);
    }
}
