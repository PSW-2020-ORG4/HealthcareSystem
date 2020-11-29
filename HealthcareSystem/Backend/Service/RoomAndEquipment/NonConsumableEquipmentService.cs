/***********************************************************************
 * Module:  NonConsumableEquipmentService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.Room&EquipmentService.NonConsumableEquipmentService
 ***********************************************************************/


using System;
using Model.Manager;
using Model.Users;
using Repository;
using System.Collections.Generic;
using Backend.Service.RoomAndEquipment;
using Backend.Repository;
using Backend.Repository.EquipmentInRoomsRepository;

namespace Service.RoomAndEquipment
{
   public class NonConsumableEquipmentService : INonConsumableEquipmentService
    {
        //private NonConsumableEquipmentRepository nonConsumableEquipmentRepository;
        private INonConsumableEquipmentRepository _nonConsumableEquipmentRepository;
        private IEquipmentInRoomsRepository _equipmentInRoomsRepository;
        public NonConsumableEquipmentService(INonConsumableEquipmentRepository nonConsumableEquipmentRepository, IEquipmentInRoomsRepository equipmentInRoomsRepository)
        {
            _nonConsumableEquipmentRepository = nonConsumableEquipmentRepository;
            _equipmentInRoomsRepository = equipmentInRoomsRepository;
        }
        public NonConsumableEquipment newNonConsumableEquipment(NonConsumableEquipment equipment)
        {
            return _nonConsumableEquipmentRepository.NewEquipment(equipment);
        }
        public List<NonConsumableEquipment> ViewNonConsumableEquipment()
    {
            return _nonConsumableEquipmentRepository.GetAllEquipment();
     }

    public Model.Manager.NonConsumableEquipment EditNonConsumableEquipment(Model.Manager.NonConsumableEquipment equipment)
    {
            return _nonConsumableEquipmentRepository.SetEquipment(equipment);
     }

    public bool DeleteNonConsumableEquipment(int id)
    {
            return _nonConsumableEquipmentRepository.DeleteEquipment(id);
     }
        public List<NonConsumableEquipment> GetNonConsumableEquipmentByRoomNumber(int roomNumber)
        {
            List<EquipmentInRooms> equipmentsInRoom = _equipmentInRoomsRepository.GetEquipmentByRoom(roomNumber);
            List<NonConsumableEquipment> nonConsumableEquipmentInRoom = new List<NonConsumableEquipment>();
            foreach (EquipmentInRooms equipmentInRoom in equipmentsInRoom)
            {
                NonConsumableEquipment nonConsumableEquipment = _nonConsumableEquipmentRepository.GetEquipment(equipmentInRoom.IdEquipment);
                if (nonConsumableEquipment != null)
                {
                    nonConsumableEquipmentInRoom.Add(nonConsumableEquipment);
                }
            }
            return nonConsumableEquipmentInRoom;
        }
    }
}