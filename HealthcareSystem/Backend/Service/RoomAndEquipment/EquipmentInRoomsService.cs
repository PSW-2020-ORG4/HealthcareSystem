/***********************************************************************
 * Module:  EquipmentInRoomsService.cs
 * Author:  LukaRA252017
 * Purpose: Definition of the Class Service.EquipmentInRooms
 ***********************************************************************/


using Model.Manager;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.RoomAndEquipment
{
    public class EquipmentInRoomsService
    {
        public EquipmentInRoomsRepository equipmentInRoomsRepository = new EquipmentInRoomsRepository();

        public Model.Manager.EquipmentInRooms addEquipmentInRoom(Model.Manager.EquipmentInRooms equipment)
        {
            return equipmentInRoomsRepository.NewEquipment(equipment);
        }

        public Model.Manager.EquipmentInRooms editEquipmentInRooms(Model.Manager.EquipmentInRooms equipment)
        {
            return equipmentInRoomsRepository.SetEquipment(equipment);
        }

        public bool deleteEquipmentInRooms(int idEquipment)
        {
            return equipmentInRoomsRepository.DeleteEquipment(idEquipment);
        }
        
        public int viewEquipmentInRooms(int idEquipment)
        {
            return equipmentInRoomsRepository.GetEquipment(idEquipment);
        }
       
    }
}
