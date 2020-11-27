/***********************************************************************
 * Module:  RoomController.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Controller.Room&EquipmentController.RoomController
 ***********************************************************************/

using Model.Enums;
using Model.Manager;
using Service.RoomAndEquipment;
using System;
using System.Collections.Generic;

namespace Controller.RoomAndEquipment
{
    public class RoomController
    {
        private RoomService _roomService;
        public int getLastId()
        {
            return _roomService.getLastId();
        }

        public void AddRoom(Room room)
        {
            // TODO: implement
            _roomService.AddRoom(room);
        }

        public void UpdateRoom(Room room)
        {
            // TODO: implement
            _roomService.UpdateRoom(room);
        }

        public void DeleteRoom(int number)
        {
            // TODO: implement
            _roomService.DeleteRoom(number);
        }

        public List<Room> ViewRooms()
        {
            // TODO: implement
            return _roomService.ViewRooms();
        }

        public Room ViewRoomByNumber(int number)
        {
            // TODO: implement
            return _roomService.ViewRoomByNumber(number);
        }

        public List<Room> ViewRoomByUsage(TypeOfUsage usage, DateTime beginDate, DateTime endDate)
        {
            return _roomService.ViewRoomByUsage(usage, beginDate, endDate);
        }

    }
}