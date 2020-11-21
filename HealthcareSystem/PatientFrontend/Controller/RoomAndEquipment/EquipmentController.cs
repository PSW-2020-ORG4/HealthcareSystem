/***********************************************************************
 * Module:  EquipmentController.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class EquipmentController
 ***********************************************************************/

using System;
using Service.RoomAndEquipment;
using Model.Manager;
using System.Collections.Generic;
using ProjekatZdravoKorporacija.Model.Manager;
using ProjekatZdravoKorporacija.Controller.RoomAndEquipment;

namespace Controller.RoomAndEquipment
{
   public class EquipmentController
{
        public NonConsumableEquipmentService nonConsumableEquipmentService = new NonConsumableEquipmentService();
        public ConsumableEquipmentService consumableEquipmentService = new ConsumableEquipmentService();

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

    public Model.Manager.NonConsumableEquipment AddNonConsumableEqipment(Model.Manager.NonConsumableEquipment equipment)
    {
            return nonConsumableEquipmentService.AddNonConsumableEqipment(equipment);
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

    public Model.Manager.ConsumableEquipment AddConsumableEqipment(Model.Manager.ConsumableEquipment equipment)
    {
            return consumableEquipmentService.AddConsumableEqipment(equipment);
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