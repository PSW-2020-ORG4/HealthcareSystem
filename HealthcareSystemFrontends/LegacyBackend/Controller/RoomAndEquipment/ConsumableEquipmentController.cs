﻿/***********************************************************************
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
    class ConsumableEquipmentController : IEquipmentController
    {
        private ConsumableEquipmentService consumableService = new ConsumableEquipmentService();
        public Equipment CreateEquipment(Equipment equipment)
        {
            return consumableService.newConsumableEquipment((ConsumableEquipment)equipment);
        }
    }
}
