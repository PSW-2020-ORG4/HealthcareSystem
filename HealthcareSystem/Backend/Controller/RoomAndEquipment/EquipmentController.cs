/***********************************************************************
 * Module:  EquipmentController.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class EquipmentController
 ***********************************************************************/

using Model.Manager;
using Service.RoomAndEquipment;
using System;
using System.Collections.Generic;

namespace Controller.RoomAndEquipment
{
    public class EquipmentController
    {
        private NonConsumableEquipmentService _nonConsumableEquipmentService;
        private ConsumableEquipmentService _consumableEquipmentService;

        public Equipment NewEquipment(TypeOfEquipment type, Equipment equipment)
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

        public List<Equipment> GetEquipmentsByRoomNumber(int roomNumber)
        {
            List<NonConsumableEquipment> nonConsumableEquipmentInRoom = _nonConsumableEquipmentService.GetNonConsumableEquipmentByRoomNumber(roomNumber);
            List<ConsumableEquipment> consumableEquipmentInRoom = _consumableEquipmentService.GetConsumableEquipmentByRoomNumber(roomNumber);
            List<Equipment> equipmentInRoom = new List<Equipment>();
            foreach (Equipment nonConsumableEquipment in nonConsumableEquipmentInRoom)
            {
                equipmentInRoom.Add(nonConsumableEquipment);
            }
            foreach (Equipment consumableEquipment in consumableEquipmentInRoom)
            {
                equipmentInRoom.Add(consumableEquipment);
            }
            return equipmentInRoom;
        }
        public List<NonConsumableEquipment> ViewNonConsumableEquipment()
        {
            return _nonConsumableEquipmentService.ViewNonConsumableEquipment();
        }

        public Model.Manager.NonConsumableEquipment EditNonConsumableEquipment(Model.Manager.NonConsumableEquipment equipment)
        {
            return _nonConsumableEquipmentService.EditNonConsumableEquipment(equipment);
        }

        public bool DeleteNonConsumableEquipment(int id)
        {
            return _nonConsumableEquipmentService.DeleteNonConsumableEquipment(id);
        }

        public List<ConsumableEquipment> ViewConsumableEquipment()
        {
            return _consumableEquipmentService.ViewConsumableEquipment();
        }
        public Model.Manager.ConsumableEquipment EditConsumableEquipment(Model.Manager.ConsumableEquipment equipment)
        {
            return _consumableEquipmentService.EditConsumableEquipment(equipment);
        }

        public bool DeleteConsumableEquipment(int id)
        {
            return _consumableEquipmentService.DeleteConsumableEquipment(id);
        }



    }
}