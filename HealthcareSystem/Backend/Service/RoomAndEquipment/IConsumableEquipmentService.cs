using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.RoomAndEquipment
{
    public interface IConsumableEquipmentService
    {
        ConsumableEquipment newConsumableEquipment(ConsumableEquipment equipment);
        List<ConsumableEquipment> ViewConsumableEquipment();
        ConsumableEquipment EditConsumableEquipment(ConsumableEquipment equipment);
        List<ConsumableEquipment> GetConsumableEquipmentByRoomNumber(int roomNumber);
        bool DeleteConsumableEquipment(int id);
    }
}
