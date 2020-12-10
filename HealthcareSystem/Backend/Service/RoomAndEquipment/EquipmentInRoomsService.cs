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
        public EquipmentInRooms AddEquipmentInRoom(EquipmentInRooms equipment)
        {
            return _equipmentInRoomsRepository.AddEquipment(equipment);
        }

        public EquipmentInRooms EditEquipmentInRooms(EquipmentInRooms equipment)
        {
            return _equipmentInRoomsRepository.UpdateEquipment(equipment);
        }

        public void DeleteEquipmentInRooms(int idEquipment)
        {
             _equipmentInRoomsRepository.DeleteEquipment(idEquipment);
        }

        public List<EquipmentInRooms> GetEquipmentInRoomsFromEquipment(Equipment equipment)
        {
            return _equipmentInRoomsRepository.GetEquipmenInRoomsByEquipmentId(equipment.Id);
        }        
    }
}
