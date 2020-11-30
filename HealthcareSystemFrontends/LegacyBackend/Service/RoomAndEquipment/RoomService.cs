/***********************************************************************
 * Module:  RoomService.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Service.RoomService
 ***********************************************************************/

using Model.Manager;
using Repository;
using System;
using System.Collections.Generic;

namespace Service.RoomAndEquipment
{
    public class RoomService
   {
        private RoomRepository roomRepository = new RoomRepository();
        private RenovationPeriodRepository renovationPeriodRepository = new RenovationPeriodRepository();
		
		public int getLastId()
        {
            return roomRepository.getLastId();
        }
		
      public Room AddRoom(Room room)
      {
            // TODO: implement
            return roomRepository.NewRoom(room);
      }
      
      public Room EditRoom(Room room)
      {
            // TODO: implement
            return roomRepository.SetRoom(room);
      }
      
      public bool DeleteRoom(int number)
      {
            // TODO: implement
            return roomRepository.DeleteRoom(number);
      }
      
      public List<Room> ViewRooms()
      {
            // TODO: implement
            return roomRepository.GetAllRooms();
      }
      
      public Room ViewRoomByNumber(int number)
      {
            // TODO: implement
            return roomRepository.GetRoomByNumber(number);
      }

	  public List<Room> ViewRoomByUsage(TypeOfUsage usage, DateTime beginDate, DateTime endDate) 
      {
            List<Room> roomsByUsage = roomRepository.GetRoomsByUsage(usage);
            List<Room> result = new List<Room>();
            foreach (Room r in roomsByUsage) {

                if (!r.Renovation)
                {
                    result.Add(r);
                }
                else
                {
                    RenovationPeriod renovationPeriod = renovationPeriodRepository.GetRenovationPeriodByRoomNumber(r.Number);
                    int beginDateCompare = DateTime.Compare(beginDate,renovationPeriod.BeginDate);
                    int endDateCompare = DateTime.Compare(endDate, renovationPeriod.EndDate);
                    if(renovationPeriod != null && ((beginDateCompare <= 0 && endDateCompare <= 0) || (beginDateCompare >= 0 && endDateCompare >= 0)))
                    {
                        result.Add(r);
                    }
                }
            }
            return result;
      }
   
   }
}