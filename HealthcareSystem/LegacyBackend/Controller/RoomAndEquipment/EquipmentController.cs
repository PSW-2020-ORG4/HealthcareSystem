/***********************************************************************
 * Module:  EquipmentController.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class EquipmentController
 ***********************************************************************/

using System;
using Service.RoomAndEquipment;
using Model.Manager;
using System.Collections.Generic;
using Controller.RoomAndEquipment;

namespace Controller.RoomAndEquipment
{
   public class EquipmentController
{
        private NonConsumableEquipmentService nonConsumableEquipmentService = new NonConsumableEquipmentService();
        private ConsumableEquipmentService consumableEquipmentService = new ConsumableEquipmentService();

        public Equipment NewEquipment(TypeOfEquipment type,Equipment equipment)
        {
            switch (type)
            {
                case TypeOfEquipment.CONSUMABLE:
                    {
                        return new ConsumableEquipmentController().CreateEquipment(equipment);
                    }
                case TypeOfEquipment.NON_CONSUMABLE:
                    {
                        return new NonConsumableEquipmentController().CreateEquipment(equipment);
                    }
                default:
                    throw new NotSupportedException();
            }
        }

    public List<NonConsumableEquipment> ViewNonConsumableEquipment()
    {
            return nonConsumableEquipmentService.ViewNonConsumableEquipment();
    }

    public Model.Manager.NonConsumableEquipment EditNonConsumableEquipment(Model.Manager.NonConsumableEquipment equipment)
    {
            return nonConsumableEquipmentService.EditNonConsumableEquipment(equipment);
    }

    public bool DeleteNonConsumableEquipment(int id)
    {
            return nonConsumableEquipmentService.DeleteNonConsumableEquipment(id);
    }

    public List<ConsumableEquipment> ViewConsumableEquipment()
    {
            return consumableEquipmentService.ViewConsumableEquipment();
    }
    public Model.Manager.ConsumableEquipment EditConsumableEquipment(Model.Manager.ConsumableEquipment equipment)
    {
            return consumableEquipmentService.EditConsumableEquipment(equipment);
    }

    public bool DeleteConsumableEquipment(int id)
    {
            return consumableEquipmentService.DeleteConsumableEquipment(id);
    }

    

}
}