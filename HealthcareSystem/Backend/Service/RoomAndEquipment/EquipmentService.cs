/***********************************************************************
 * Module:  ConsumableEquipmentService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.ConsumableEquipmentService
 ***********************************************************************/

using Backend.Repository;
using Backend.Repository.EquipmentInRoomsRepository;
using Backend.Service.RoomAndEquipment;
using Model.Manager;
using System.Collections.Generic;

namespace Service.RoomAndEquipment
{
    public class EquipmentService : IEquipmentService
    {
        private IEquipmentRepository _equipmentRepository;
        private IEquipmentInRoomsRepository _equipmentInRoomRepository;
        public EquipmentService(IEquipmentRepository equipmentRepository, IEquipmentInRoomsRepository equipmentInRoomRepository)
        {
            _equipmentRepository = equipmentRepository;
            _equipmentInRoomRepository = equipmentInRoomRepository;
        }
        public Equipment AddEquipment(Equipment equipment)
        {
            return _equipmentRepository.AddEquipment(equipment);
        }
        public List<Equipment> GetEquipment()
        {
            return _equipmentRepository.GetAllEquipment();
        }

        public Model.Manager.Equipment EditEquipment(Model.Manager.Equipment equipment)
        {
            return _equipmentRepository.SetEquipment(equipment);
        }

        public bool DeleteEquipment(int id)
        {
            return _equipmentRepository.DeleteEquipment(id);
        }

        public List<Equipment> GetEquipmentByRoomNumber(int roomNumber)
        {
            List<EquipmentInRooms> equipmentsInRoom = _equipmentInRoomRepository.GetEquipmentByRoom(roomNumber);
            List<Equipment> equipmentInRoom = new List<Equipment>();
            foreach (EquipmentInRooms singleEquipment in equipmentsInRoom)
            {
                Equipment anEquipment = _equipmentRepository.GetEquipment(singleEquipment.IdEquipment);
                if (anEquipment != null)
                {
                    equipmentInRoom.Add(anEquipment);
                }
            }
            return equipmentInRoom;
        }



    }
}