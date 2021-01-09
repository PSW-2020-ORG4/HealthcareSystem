/***********************************************************************
 * Module:  RoomService.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Service.RoomService
 ***********************************************************************/

using Backend.Model.Enums;
using Backend.Model.Exceptions;
using Backend.Model.Manager;
using Backend.Repository;
using Backend.Repository.EquipmentInRoomsRepository;
using Backend.Repository.RenovationPeriodRepository;
using Backend.Repository.RoomRepository;
using Backend.Service;
using Model.Manager;
using System;
using System.Collections.Generic;

namespace Backend.Service.RoomAndEquipment
{
    public class RoomService : IRoomService
    {
        private IRenovationPeriodRepository _renovationPeriodRepository;
        private IRoomRepository _roomRepository;
        private IEquipmentInRoomsRepository _equipmentInRoomsRepository;
        private IEquipmentRepository _equipmentRepository;

        public RoomService(
            IRoomRepository roomRepository,
            IRenovationPeriodRepository renovationPeriodRepository,
            IEquipmentInRoomsRepository equipmentInRoomsRepository,
            IEquipmentRepository equipmentRepository)
        {
            _renovationPeriodRepository = renovationPeriodRepository;
            _roomRepository = roomRepository;
            _equipmentInRoomsRepository = equipmentInRoomsRepository;
            _equipmentRepository = equipmentRepository;
        }

        //private RoomRepository roomRepository = new RoomRepository();
        //private RenovationPeriodRepository renovationPeriodRepository = new RenovationPeriodRepository();

        public int getLastId()
        {
            int id = _roomRepository.getLastId();
            if (id == 0)
                throw new NotFoundException("Rooms do not exist in database.");
            return id;
        }

        public void AddRoom(Room room)
        {
            // TODO: implement
            _roomRepository.AddRoom(room);
        }

        public void UpdateRoom(Room room)
        {
            // TODO: implement
            _roomRepository.UpdateRoom(room);
        }

        public void DeleteRoom(int number)
        {
            // TODO: implement
            _roomRepository.DeleteRoom(number);
        }

        public List<Room> ViewRooms()
        {
            // TODO: implement
            return _roomRepository.GetAllRooms();
        }

        public Room ViewRoomByNumber(int number)
        {
            // TODO: implement
            return _roomRepository.GetRoomByNumber(number);
        }

        public List<Room> ViewRoomByUsage(TypeOfUsage usage, DateTime beginDate, DateTime endDate)
        {
            List<Room> roomsByUsage = _roomRepository.GetRoomsByUsage(usage);
            List<Room> result = new List<Room>();
            foreach (Room r in roomsByUsage)
            {

                if (!r.Renovation)
                {
                    result.Add(r);
                }
                else
                {
                    RenovationPeriod renovationPeriod = _renovationPeriodRepository.GetRenovationPeriodByRoomNumber(r.Id);
                    int beginDateCompare = DateTime.Compare(beginDate, renovationPeriod.BeginDate);
                    int endDateCompare = DateTime.Compare(endDate, renovationPeriod.EndDate);
                    if (renovationPeriod != null && (beginDateCompare <= 0 && endDateCompare <= 0 || beginDateCompare >= 0 && endDateCompare >= 0))
                    {
                        result.Add(r);
                    }
                }
            }
            return result;
        }

        public ICollection<Room> GetRoomsByUsageAndEquipment(TypeOfUsage usage, ICollection<int> requiredEquipmentTypeIds)
        {
            List<Room> allRooms = _roomRepository.GetAllRooms();
            allRooms = allRooms.FindAll(r => r.Usage == usage);

            ICollection<Room> validRooms = new List<Room>();
            foreach (Room room in allRooms)
            {
                ICollection<int> availableEquipmentTypes = GetAvailableEquipmentTypesInRoom(room);
                if (CheckIfRoomHasRequiredEquipment(requiredEquipmentTypeIds, availableEquipmentTypes))
                    validRooms.Add(room);
            }

            return validRooms;
        }

        private ICollection<int> GetAvailableEquipmentTypesInRoom(Room room)
        {
            List<EquipmentInRooms> equipmentInRooms = _equipmentInRoomsRepository.GetEquipmentInRoomsByRoomNumber(room.Id);
            ICollection<int> availableEquipmentTypes = new List<int>();
            foreach (EquipmentInRooms equipmentInRoom in equipmentInRooms)
            {
                Equipment equipment = _equipmentRepository.GetEquipmentById(equipmentInRoom.IdEquipment);
                EquipmentType equipmentType = equipment.Type;
                if (!availableEquipmentTypes.Contains(equipmentType.Id))
                    availableEquipmentTypes.Add(equipmentType.Id);
            }

            return availableEquipmentTypes;
        }

        private bool CheckIfRoomHasRequiredEquipment(ICollection<int> requiredEquipmentTypeIds, ICollection<int> availableEquipmentTypes)
        {
            foreach (int requiredEquipmentTypeId in requiredEquipmentTypeIds)
            {
                if (!availableEquipmentTypes.Contains(requiredEquipmentTypeId))
                    return false;
            }

            return true;
        }

        public bool CheckIfRoomExists(int roomId)
        {
            return _roomRepository.CheckIfRoomExists(roomId);
        }
    }
}