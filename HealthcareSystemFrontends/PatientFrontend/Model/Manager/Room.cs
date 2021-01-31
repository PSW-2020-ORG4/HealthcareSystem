/***********************************************************************
 * Module:  Room.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Manager.Room
 ***********************************************************************/

using System;

namespace Model.Manager
{
   public class Room
   {
        public int Number { get; set; }
        public TypeOfUsage Usage { get; set; }
        public int Capacity { get; set; }
        public int Occupation { get; set; }
        public bool Renovation { get; set; }

        public Room() { }

        public Room(int number,TypeOfUsage typeOfUsage,int capacity,int occupation,bool renovation)
        {
            this.Number = number;
            this.Usage = typeOfUsage;
            this.Capacity = capacity;
            this.Occupation = occupation;
            this.Renovation = renovation;
        }
        public Room(Room room)
        {
            this.Number = room.Number;
            this.Usage = room.Usage;
            this.Capacity = room.Capacity;
            this.Occupation = room.Occupation;
            this.Renovation = room.Renovation;
        }

    }
}