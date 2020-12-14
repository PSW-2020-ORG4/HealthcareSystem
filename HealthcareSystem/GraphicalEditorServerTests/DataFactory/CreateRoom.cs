using Model.Enums;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEditorServerTests.DataFactory
{
    public class CreateRoom
    {
        public List<Room> CreateRooms()
        {
            Room room1 = new Room(0, TypeOfUsage.CONSULTING_ROOM, 20, 10, false);
            Room room2 = new Room(1, TypeOfUsage.CONSULTING_ROOM, 20, 10, false);
            Room room3 = new Room(2, TypeOfUsage.CONSULTING_ROOM, 20, 10, false);

            List<Room> rooms = new List<Room>();
            rooms.Add(room1);
            rooms.Add(room2);
            rooms.Add(room3);

            return rooms;
        }
    }
}
