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
        private IConsumableEquipmentService _consumableEquipmentService;
        private INonConsumableEquipmentService _nonConsumableEquipmentService;
        public EquipmentInRoomsService(IEquipmentInRoomsRepository equipmentInRoomsRepository, IConsumableEquipmentService consumableEquipmentService, INonConsumableEquipmentService nonConsumableEquipmentService)
        {
            _equipmentInRoomsRepository = equipmentInRoomsRepository;
            _consumableEquipmentService = consumableEquipmentService;
            _nonConsumableEquipmentService = nonConsumableEquipmentService;

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

    }
}
