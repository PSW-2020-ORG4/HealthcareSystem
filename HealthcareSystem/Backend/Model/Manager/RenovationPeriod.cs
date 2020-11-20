/***********************************************************************
 * Module:  Renovation.cs
 * Author:  LukaRA252017
 * Purpose: Definition of the Class Manager.Renovation
 ***********************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Manager
{
    public class RenovationPeriod
    {
        [ForeignKey("Room")]
        public int RoomNumber { get; set; }
        public virtual Room Room { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public RenovationPeriod()
        {

        }
        public RenovationPeriod(Room room, DateTime begin, DateTime end)
        {
            if (room == null)
            {
                this.Room = new Room();
            }
            else
            {
                this.Room = new Room(room);
            }
            BeginDate = begin;
            EndDate = end;
        }
        public RenovationPeriod(RenovationPeriod renovationPeriod)
        {
            if (renovationPeriod.Room == null)
            {
                Room = new Room();
            }
            else
            {
                Room = new Room(renovationPeriod.Room);
            }
            Room = new Room(renovationPeriod.Room);
            BeginDate = renovationPeriod.BeginDate;
            EndDate = renovationPeriod.EndDate;
        }
    }
}
