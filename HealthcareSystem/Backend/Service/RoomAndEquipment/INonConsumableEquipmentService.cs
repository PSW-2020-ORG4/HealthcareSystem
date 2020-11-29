using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.RoomAndEquipment
{
   public interface INonConsumableEquipmentService
    {
        NonConsumableEquipment newNonConsumableEquipment(NonConsumableEquipment equipment);
        NonConsumableEquipment EditNonConsumableEquipment(NonConsumableEquipment equipment);
        bool DeleteNonConsumableEquipment(int id);
        List<NonConsumableEquipment> GetNonConsumableEquipmentByRoomNumber(int roomNumber);
    }
}
