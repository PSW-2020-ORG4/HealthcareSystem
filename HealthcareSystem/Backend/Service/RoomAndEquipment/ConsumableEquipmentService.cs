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
    public class ConsumableEquipmentService : IConsumableEquipmentService
    {
        private IConsumableEquipmentRepository _consumableEquipmentRepository;
        private IEquipmentInRoomsRepository _equipmentInRoomRepository;
        public ConsumableEquipmentService(IConsumableEquipmentRepository consumableEquipmentRepository, IEquipmentInRoomsRepository equipmentInRoomRepository)
        {
            _consumableEquipmentRepository = consumableEquipmentRepository;
            _equipmentInRoomRepository = equipmentInRoomRepository;
        }
        public ConsumableEquipment newConsumableEquipment(ConsumableEquipment equipment)
        {
            return _consumableEquipmentRepository.NewEquipment(equipment);
        }
        public List<ConsumableEquipment> ViewConsumableEquipment()
        {
            return _consumableEquipmentRepository.GetAllEquipment();
        }

        public Model.Manager.ConsumableEquipment EditConsumableEquipment(Model.Manager.ConsumableEquipment equipment)
        {
            return _consumableEquipmentRepository.SetEquipment(equipment);
        }

        public bool DeleteConsumableEquipment(int id)
        {
            return _consumableEquipmentRepository.DeleteEquipment(id);
        }

        public List<ConsumableEquipment> GetConsumableEquipmentByRoomNumber(int roomNumber)
        {
            List<EquipmentInRooms> equipmentsInRoom = _equipmentInRoomRepository.GetEquipmentByRoom(roomNumber);
            List<ConsumableEquipment> consumableEquipmentInRoom = new List<ConsumableEquipment>();
            foreach (EquipmentInRooms equipmentInRoom in equipmentsInRoom)
            {
                ConsumableEquipment consumableEquipment = _consumableEquipmentRepository.GetEquipment(equipmentInRoom.IdEquipment);
                if (consumableEquipment != null)
                {
                    consumableEquipmentInRoom.Add(consumableEquipment);
                }
            }
            return consumableEquipmentInRoom;
        }



    }
}