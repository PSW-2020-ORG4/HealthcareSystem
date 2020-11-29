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
        List<EquipmentInRooms> GetEquipmentByRoom(int roomNumber);
        EquipmentInRooms GetEquipment(int idEquipment);
        EquipmentInRooms SetEquipment(EquipmentInRooms equipment);
        bool DeleteEquipment(int id);
        EquipmentInRooms NewEquipment(EquipmentInRooms equipment);

    }
}
