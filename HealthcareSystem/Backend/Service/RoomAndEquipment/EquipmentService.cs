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

        public void DeleteEquipment(int id)
        {
             _equipmentRepository.DeleteEquipment(id);
        }

        public List<Equipment> GetEquipmentWithRoomForSearchTerm(string searchTerm)
        {
            List<Equipment> equipment = GetEquipment();
            List<Equipment> validEquipment = new List<Equipment>();
            foreach (Equipment e in equipment)
            {
                if (CheckIfEquipmentNameContainsSearchTerm(e, searchTerm))
                    validEquipment.Add(e);
            }

            return validEquipment;
        }

        private bool CheckIfEquipmentNameContainsSearchTerm(Equipment equipment, string searchTerm)
        {
            if (equipment.Type.Name.ToString().ToLower().Contains(searchTerm.ToLower()))
                return true;
            else return false;
        }

        public Equipment GetEquipmentById(int equipmentId)
        {
            return _equipmentRepository.GetEquipmentById(equipmentId);
        }

        public List<Equipment> GetEquipmentByRoomNumber(int roomNumber)
        {
            List<EquipmentInRooms> equipmentInRooms = _equipmentInRoomRepository.GetEquipmentInRoomsByRoomNumber(roomNumber);
            if (equipmentInRooms == null) 
            {
                return null;
            }
            List<Equipment> equipment = new List<Equipment>();
            foreach (var e in equipmentInRooms)
            {
                equipment.Add(GetEquipmentById(e.IdEquipment));
            }

            return equipment;
        }
    }
}