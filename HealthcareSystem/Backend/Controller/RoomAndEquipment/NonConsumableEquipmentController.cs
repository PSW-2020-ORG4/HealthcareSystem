/***********************************************************************
 * Author:  Sladjana Savkovic
 ***********************************************************************/

using Model.Manager;
using Service.RoomAndEquipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.RoomAndEquipment
{
    class NonConsumableEquipmentController : IEquipmentController
    {
        private NonConsumableEquipmentService _nonConsumableService; 
        public Equipment CreateEquipment(Equipment equipment)
        {
            return _nonConsumableService.newNonConsumableEquipment((NonConsumableEquipment)equipment);
        }
    }
}
