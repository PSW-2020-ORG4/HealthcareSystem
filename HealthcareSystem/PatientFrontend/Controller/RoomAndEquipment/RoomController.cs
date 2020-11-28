/***********************************************************************
 * Module:  RoomController.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Controller.Room&EquipmentController.RoomController
 ***********************************************************************/

using Model.Manager;
using Service.RoomAndEquipment;
using System;
using System.Collections.Generic;

namespace Controller.RoomAndEquipment
{
   public class RoomController
   {
        private RoomService roomService = new RoomService();
		
		public int getLastId()
        {
            return roomService.getLastId();
        }
		
      public Room AddRoom(Room room)
      {
            // TODO: implement
            return roomService.AddRoom(room);
      }
      
      public Room EditRoom(Room room)
      {
            // TODO: implement
            return roomService.EditRoom(room);
      }
      
      public bool DeleteRoom(int number)
      {
            // TODO: implement
            return roomService.DeleteRoom(number);
      }
      
      public List<Room> ViewRooms()
      {
            // TODO: implement
            return roomService.ViewRooms();
      }
      
      public Room ViewRoomByNumber(int number)
      {
            // TODO: implement
            return roomService.ViewRoomByNumber(number);
      }
	  
	  public List<Room> ViewRoomByUsage(TypeOfUsage usage, DateTime date)
      {
            return roomService.ViewRoomByUsage(usage, date);
      }
   
   }
}