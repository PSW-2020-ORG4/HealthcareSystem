/***********************************************************************
 * Module:  Renovation.cs
 * Author:  LukaRA252017
 * Purpose: Definition of the Class Manager.Renovation
 ***********************************************************************/

using System;

namespace Model.Manager
{
    public class RenovationPeriod
    {
        public Room room { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public RenovationPeriod()
        {

        }
		public RenovationPeriod(Room room,DateTime begin,DateTime end)
        {
            if (room == null)
            {
                this.room = new Room();
            }
            else
            {
                this.room = new Room(room);
            }
            BeginDate = begin;
            EndDate = end;
        }
        public RenovationPeriod(RenovationPeriod renovationPeriod)
        {
            if (renovationPeriod.room == null)
            {
                room = new Room();
            }
            else
            {
                room = new Room(renovationPeriod.room);
            }
            room = new Room(renovationPeriod.room);
            BeginDate = renovationPeriod.BeginDate;
            EndDate = renovationPeriod.EndDate;
        }
    }
}
