using Model.Manager;
using Service.RoomAndEquipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatZdravoKorporacija.Controller.RoomAndEquipment
{
    class NonConsumableEquipmentController : IEquipmentController
    {
        private NonConsumableEquipmentService nonConsumableService = new NonConsumableEquipmentService();
        public Equipment CreateEquipment(Equipment equipment)
        {
            return nonConsumableService.newNonConsumableEquipment((NonConsumableEquipment)equipment);
        }
    }
}
