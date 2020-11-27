/***********************************************************************
 * Module:  EquipmentInRoomsRepository.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class Repository.EquipmentInRoomsRepository
 ***********************************************************************/

using System;
using Model.Manager;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Backend.Repository.EquipmentInRoomsRepository;

namespace Repository
{
    public class EquipmentInRoomsRepository : IEquipmentInRoomsRepository
    {
        private string path;

        public EquipmentInRoomsRepository()
        {

            string fileName = "equipmentInRooms.json";
            path = Path.GetFullPath(fileName);
        }

        public List<EquipmentInRooms> GetEquipmentByRoom(int roomNumber)
        {
            List<EquipmentInRooms> equipmentInRooms = ReadFromFile();
            List<EquipmentInRooms> results = new List<EquipmentInRooms>();
            foreach (EquipmentInRooms e in equipmentInRooms)
            {
                if (e.RoomNumber==roomNumber)
                {
                    results.Add(e);
                }
            }
            return results;
        }
        
        public int GetEquipment(int idEquipment)
        {
            List<EquipmentInRooms> equipmentInRooms = ReadFromFile();
            int res;
            foreach (EquipmentInRooms e in equipmentInRooms)
            {
                if (e.IdEquipment== idEquipment)
                {
                    res=e.RoomNumber;
                    return res;
                }
            }
            return 0;
        }

        public Model.Manager.EquipmentInRooms SetEquipment(Model.Manager.EquipmentInRooms equipment)
        {
            List<EquipmentInRooms> equipmentInRooms = ReadFromFile();

            foreach (EquipmentInRooms e in equipmentInRooms)
            {
                if (e.IdEquipment==equipment.IdEquipment)
                {
                  
                    e.Quantity = equipment.Quantity;
                    e.RoomNumber = equipment.RoomNumber;
                    break;
                }
            }
            WriteInFile(equipmentInRooms);
            return equipment;
        }

        public bool DeleteEquipment(int id)
        {
            List<EquipmentInRooms> equipmentInRooms = ReadFromFile();
            EquipmentInRooms equipmentForDelete = null;
            foreach (EquipmentInRooms e in equipmentInRooms)
            {
                if (e.IdEquipment==id)
                {
                    equipmentForDelete = e;
                    break;
                }
            }
            if (equipmentForDelete == null)
            {
                return false;
            }

            equipmentInRooms.Remove(equipmentForDelete);
            WriteInFile(equipmentInRooms);
            return true;
        }
        
        public Model.Manager.EquipmentInRooms NewEquipment(Model.Manager.EquipmentInRooms equipment)
        {
            List<EquipmentInRooms> equipmentInRooms = ReadFromFile();
            foreach (EquipmentInRooms e in equipmentInRooms)
            {
                if (e.IdEquipment==equipment.IdEquipment)
                {
                    return null;
                }
            }
            
            equipmentInRooms.Add(equipment);
            WriteInFile(equipmentInRooms);
            return equipment;
        }

        public List<EquipmentInRooms> ReadFromFile()
        {
            List<EquipmentInRooms> equipmentInRooms;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                equipmentInRooms = JsonConvert.DeserializeObject<List<EquipmentInRooms>>(json);
            }
            else
            {
                equipmentInRooms = new List<EquipmentInRooms>();
                WriteInFile(equipmentInRooms);
            }
            return equipmentInRooms;
        }

        private void WriteInFile(List<EquipmentInRooms> equipmentInRooms)
        {
            string json = JsonConvert.SerializeObject(equipmentInRooms);
            File.WriteAllText(path, json);
        }

        EquipmentInRooms IEquipmentInRoomsRepository.GetEquipment(int idEquipment)
        {
            throw new NotImplementedException();
        }
    }
}