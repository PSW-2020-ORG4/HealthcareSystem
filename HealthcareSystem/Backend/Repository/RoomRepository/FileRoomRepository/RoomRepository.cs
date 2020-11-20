/***********************************************************************
 * Module:  RoomRepository.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Repository.RoomRepository
 ***********************************************************************/

using Backend.Repository.RoomRepository;
using Model.Enums;
using Model.Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Repository
{
    public class RoomRepository : IRoomRepository
    {
        private string path;

        public RoomRepository()
        {
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
            return rooms[rooms.Count - 1].Id;
        }

        public Room GetRoomByNumber(int number)
        {
            // TODO: implement
            List<Room> roomList = ReadFromFile();
            foreach (Room r in roomList)
            {
                if (r.Id == number)
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

        public void UpdateRoom(Room room)
        {
            // TODO: implement
            List<Room> roomList = ReadFromFile();
            foreach (Room r in roomList)
            {
                if (r.Id == room.Id)
                {
                    r.Id = room.Id;
                    r.Usage = room.Usage;
                    r.Capacity = room.Capacity;
                    r.Occupation = room.Occupation;
                    r.Renovation = room.Renovation;
                    break;
                }
            }
            WriteInFile(roomList);

        }

        public void DeleteRoom(int number)
        {
            // TODO: implement
            List<Room> roomList = ReadFromFile();
            Room roomForDelete = null;
            foreach (Room r in roomList)
            {
                if (r.Id == number)
                {
                    roomForDelete = r;
                    break;
                }
            }
            if (roomForDelete == null)
                throw new ValidationException();
            roomList.Remove(roomForDelete);
            WriteInFile(roomList);

        }

        public void AddRoom(Room room)
        {
            // TODO: implement
            List<Room> roomList = ReadFromFile();
            Room searchRoom = GetRoomByNumber(room.Id);
            if (searchRoom != null)
            {
                throw new ValidationException();
            }
            roomList.Add(room);
            WriteInFile(roomList);

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