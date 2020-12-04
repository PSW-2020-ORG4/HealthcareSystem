using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.RoomAndEquipment
{
    public interface IEquipmentService
    {
        Equipment AddEquipment(Equipment equipment);
        List<Equipment> GetEquipment();
        Equipment EditEquipment(Equipment equipment);
        Equipment GetEquipmentById(int equipmentId);
        bool DeleteEquipment(int id);
        List<Equipment> GetEquipmentWithRoomForSearchTerm(string searchTerm);
        List<Equipment> GetEquipmentByRoomNumber(int roomNumber);
    }
}
