/***********************************************************************
 * Module:  EquipmentInRoomsService.cs
 * Author:  LukaRA252017
 * Purpose: Definition of the Class Service.EquipmentInRooms
 ***********************************************************************/


using Backend.Repository.EquipmentInRoomsRepository;
using Backend.Service.RoomAndEquipment;
using Model.Manager;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.RoomAndEquipment
{
    public class EquipmentInRoomsService : IEquipmentInRoomsService
    {
        private IEquipmentInRoomsRepository _equipmentInRoomsRepository;
        private IEquipmentService _equipmentService;
        public EquipmentInRoomsService(IEquipmentInRoomsRepository equipmentInRoomsRepository, IEquipmentService equipmentService)
        {
            _equipmentInRoomsRepository = equipmentInRoomsRepository;
            _equipmentService = equipmentService;

        }
        public EquipmentInRooms addEquipmentInRoom(EquipmentInRooms equipment)
        {
            return _equipmentInRoomsRepository.NewEquipment(equipment);
        }

        public EquipmentInRooms editEquipmentInRooms(EquipmentInRooms equipment)
        {
            return _equipmentInRoomsRepository.SetEquipment(equipment);
        }

        public bool deleteEquipmentInRooms(int idEquipment)
        {
            return _equipmentInRoomsRepository.DeleteEquipment(idEquipment);
        }
        
        public EquipmentInRooms viewEquipmentInRooms(int idEquipment)
        {
            return _equipmentInRoomsRepository.GetEquipment(idEquipment);
        }
        public List<Equipment> getEquipmentByRoomNumber(int roomNumber)
        {
            return _equipmentService.GetEquipmentByRoomNumber(roomNumber);
        }
    }
}
