/***********************************************************************
 * Module:  RoomService.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Service.RoomService
 ***********************************************************************/

using Backend.Model.Exceptions;
using Backend.Repository.RenovationPeriodRepository;
using Backend.Repository.RoomRepository;
using Backend.Service;
using Backend.Service.RoomAndEquipment;
using Model.Enums;
using Model.Manager;
using Repository;
using System;
using System.Collections.Generic;

namespace Service.RoomAndEquipment
{
    public class RoomService : IRoomService
    {
        private IRenovationPeriodRepository _renovationPeriodRepository;
        private IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository, IRenovationPeriodRepository renovationPeriodRepository)
        {
            _renovationPeriodRepository = renovationPeriodRepository;
            _roomRepository = roomRepository;
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
                    RenovationPeriod renovationPeriod = _renovationPeriodRepository.GetRenovationPeriodByRoomNumber(r.Number);
                    int beginDateCompare = DateTime.Compare(beginDate, renovationPeriod.BeginDate);
                    int endDateCompare = DateTime.Compare(endDate, renovationPeriod.EndDate);
                    if (renovationPeriod != null && ((beginDateCompare <= 0 && endDateCompare <= 0) || (beginDateCompare >= 0 && endDateCompare >= 0)))
                    {
                        result.Add(r);
                    }
                }
            }
            return result;
        }

    }
}