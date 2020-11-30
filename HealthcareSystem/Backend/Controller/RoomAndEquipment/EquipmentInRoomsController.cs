/***********************************************************************
 * Module:  EquipmentInRoomsController.cs
 * Author:  LukaRA252017
 * Purpose: Definition of the Class Controller.EquipmentInRoomsController
 ***********************************************************************/

using Model.Manager;
using Service.RoomAndEquipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller.RoomAndEquipment
{
    public class EquipmentInRoomsController
    {
        public EquipmentInRoomsService _equipmentInRoomsService;
        public Model.Manager.EquipmentInRooms addEquipmentInRoom(Model.Manager.EquipmentInRooms equipment)
        {
            return _equipmentInRoomsService.addEquipmentInRoom(equipment);
        }

        public Model.Manager.EquipmentInRooms editEquipmentInRooms(Model.Manager.EquipmentInRooms equipment)
        {
            return _equipmentInRoomsService.editEquipmentInRooms(equipment);
        }

        public bool deleteEquipmentInRooms(int idEquipment)
        {
            return _equipmentInRoomsService.deleteEquipmentInRooms(idEquipment);
        }

        public int viewEquipmentInRooms(int roomNumber)
        {
            return _equipmentInRoomsService.viewEquipmentInRooms(roomNumber);
        }
    }
}
