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
        List<Equipment> GetEquipmentByRoomNumber(int roomNumber);
        bool DeleteEquipment(int id);
    }
}
