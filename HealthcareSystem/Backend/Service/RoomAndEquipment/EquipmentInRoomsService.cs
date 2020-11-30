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
        public EquipmentInRoomsRepository equipmentInRoomsRepository = new EquipmentInRoomsRepository();

        private IEquipmentInRoomsRepository _equipmentInRoomsRepository;
        private IConsumableEquipmentService _consumableEquipmentService;
        private INonConsumableEquipmentService _nonConsumableEquipmentService;
        public EquipmentInRoomsService(IEquipmentInRoomsRepository equipmentInRoomsRepository, IConsumableEquipmentService consumableEquipmentService, INonConsumableEquipmentService nonConsumableEquipmentService)
        {
            _equipmentInRoomsRepository = equipmentInRoomsRepository;
            _consumableEquipmentService = consumableEquipmentService;
            _nonConsumableEquipmentService = nonConsumableEquipmentService;

        }
        public Model.Manager.EquipmentInRooms addEquipmentInRoom(Model.Manager.EquipmentInRooms equipment)
        {
            return _equipmentInRoomsRepository.NewEquipment(equipment);
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
