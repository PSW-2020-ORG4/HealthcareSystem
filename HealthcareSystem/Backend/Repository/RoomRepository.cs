/***********************************************************************
 * Module:  RoomRepository.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Repository.RoomRepository
 ***********************************************************************/

using Model.Enums;
using Model.Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class RoomRepository
   {
        private string path;

        public RoomRepository() {
            string fileName = "room.json";
            path = Path.GetFullPath(fileName);

        }
		
		 public int getLastId()
        {
            List<Room> rooms = ReadFromFile();
            if (rooms.Count == 0)
            {
                return 0;
            }
            return rooms[rooms.Count - 1].Number;
        }
		
        public Room GetRoomByNumber(int number)
      {
            // TODO: implement
            List<Room> roomList = ReadFromFile();
            foreach (Room r in roomList)
            {
                if (r.Number == number)
                {
                    return r;
                }
            }
            return null;

        }
		
		public List<Room> GetRoomsByUsage(TypeOfUsage usage) 
		{

            List<Room> roomList = ReadFromFile();
            List<Room> result = new List<Room>();
            foreach (Room r in roomList)
            {
                if (r.Usage.Equals(usage))
                {
                    result.Add(r);
                }
            }
            return result;

        }

        public List<Room> GetAllRooms()
      {
            // TODO: implement
            List<Room> roomList = ReadFromFile();
            return roomList;

      }
      
      public Room SetRoom(Room room)
      {
            // TODO: implement
            List<Room> roomList = ReadFromFile();
            foreach (Room r in roomList)
            {
                if (r.Number == room.Number)
                {
                    r.Number = room.Number;
                    r.Usage = room.Usage;
                    r.Capacity = room.Capacity;
                    r.Occupation = room.Occupation;
                    r.Renovation = room.Renovation;
                    break;
                }
            }
            WriteInFile(roomList);
            return room;

        }

        public bool DeleteRoom(int number)
      {
            // TODO: implement
            List<Room> roomList = ReadFromFile();
            Room roomForDelete = null;
            foreach (Room r in roomList)
            {
                if (r.Number == number)
                {
                    roomForDelete = r;
                    break;
                }
            }
            if (roomForDelete == null)
            {
                return false;
            }
            roomList.Remove(roomForDelete);
            WriteInFile(roomList);
            return true;

        }

        public Room NewRoom(Room room)
         {
            // TODO: implement
            List<Room> roomList = ReadFromFile();
            Room searchRoom = GetRoomByNumber(room.Number);
            if (searchRoom != null)
            {
                return null;
            }
            roomList.Add(room);
            WriteInFile(roomList);
            return room;

        }

        private List<Room> ReadFromFile()
        {
            List<Room> roomList;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                roomList = JsonConvert.DeserializeObject<List<Room>>(json);
            }
            else
            {
                roomList = new List<Room>();
                WriteInFile(roomList);
            }
            return roomList;
        }

        private void WriteInFile(List<Room> roomList)
        {
            string json = JsonConvert.SerializeObject(roomList);
            File.WriteAllText(path, json);
        }




    }
}