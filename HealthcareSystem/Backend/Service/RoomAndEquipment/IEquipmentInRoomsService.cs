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
        EquipmentInRooms AddEquipmentInRoom(EquipmentInRooms equipment);
        EquipmentInRooms EditEquipmentInRooms(EquipmentInRooms equipment);
        void DeleteEquipmentInRooms(int idEquipment);
        List<EquipmentInRooms> GetEquipmentInRoomsFromEquipment(Equipment equipment);
    }
}
