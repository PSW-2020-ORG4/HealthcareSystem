/***********************************************************************
 * Module:  Room.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Manager.Room
 ***********************************************************************/

using Backend.Model.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Manager
{
    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public TypeOfUsage Usage { get; set; }
        public int Capacity { get; set; }
        public int Occupation { get; set; }
        public bool Renovation { get; set; }

        public Room() { }

        public Room(int number, TypeOfUsage typeOfUsage, int capacity, int occupation, bool renovation)
        {
            Id = number;
            Usage = typeOfUsage;
            Capacity = capacity;
            Occupation = occupation;
            Renovation = renovation;
        }
        public Room(Room room)
        {
            Id = room.Id;
            Usage = room.Usage;
            Capacity = room.Capacity;
            Occupation = room.Occupation;
            Renovation = room.Renovation;
        }

    }
}