/***********************************************************************
 * Author:  Sladjana Savkovic
 ***********************************************************************/

using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.RoomAndEquipment
{
    public interface IEquipmentController
    {
        Equipment CreateEquipment(Equipment equipment);
    }
}
