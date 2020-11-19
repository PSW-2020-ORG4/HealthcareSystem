/***********************************************************************
 * Module:  Room.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Manager.Room
 ***********************************************************************/

using Model.Enums;
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
            Number = number;
            Usage = typeOfUsage;
            Capacity = capacity;
            Occupation = occupation;
            Renovation = renovation;
        }
        public Room(Room room)
        {
            Number = room.Number;
            Usage = room.Usage;
            Capacity = room.Capacity;
            Occupation = room.Occupation;
            Renovation = room.Renovation;
        }

    }
}