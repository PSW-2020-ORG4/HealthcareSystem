using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
