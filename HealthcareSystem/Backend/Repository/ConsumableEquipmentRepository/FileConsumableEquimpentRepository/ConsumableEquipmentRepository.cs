/***********************************************************************
 * Module:  ConsumableEquipmentRepository.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class Repository.ConsumableEquipmentRepository
 ***********************************************************************/

using System;
using Model.Manager;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Backend.Repository;

namespace Repository
{
    public class ConsumableEquipmentRepository : IConsumableEquipmentRepository
    {
        private string path;

        public ConsumableEquipmentRepository()
        {

            string fileName = "consumableEquipment.json";
            path = Path.GetFullPath(fileName);
        }

        public Model.Manager.ConsumableEquipment GetEquipment(int id)
        {
            List<ConsumableEquipment> consumableEquipment = ReadFromFile();
            foreach (ConsumableEquipment e in consumableEquipment)
            {
                if (e.Id.Equals(id))
                {
                    return e;
                }
            }

            return null;
        }

        public List<ConsumableEquipment> GetAllEquipment()
        {
            List<ConsumableEquipment> consumableEquipment = ReadFromFile();
            return consumableEquipment;
        }

        public Model.Manager.ConsumableEquipment SetEquipment(Model.Manager.ConsumableEquipment equipment)
        {
            List<ConsumableEquipment> consumableEquipment = ReadFromFile();

            foreach (ConsumableEquipment e in consumableEquipment)
            {
                if (e.Id.Equals(equipment.Id))
                {
                    
                    e.Quantity = equipment.Quantity;
                    e.Type = equipment.Type; 
                    break;
                }
            }
            WriteInFile(consumableEquipment);
            return equipment;
        }

        public bool DeleteEquipment(int id)
        {
            List<ConsumableEquipment> consumableEquipment = ReadFromFile();
            ConsumableEquipment equipmentForDelete = null;
            foreach (ConsumableEquipment e in consumableEquipment)
            {
                if (e.Id.Equals(id))
                {
                    equipmentForDelete = e;
                    break;
                }
            }
            if (equipmentForDelete == null)
            {
                return false;
            }

            consumableEquipment.Remove(equipmentForDelete);
            WriteInFile(consumableEquipment);
            return true;
        }

        public ConsumableEquipment NewEquipment(ConsumableEquipment equipment)
        {
            List<ConsumableEquipment> consumableEquipment = ReadFromFile();
            ConsumableEquipment searchEquipment = GetEquipment(equipment.Id);
            if (searchEquipment != null)
            {
                return null;
            }

            consumableEquipment.Add(equipment);
            WriteInFile(consumableEquipment);
            return equipment;
        }

        private List<ConsumableEquipment> ReadFromFile()
        {
            List<ConsumableEquipment> consumableEquipment;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                consumableEquipment = JsonConvert.DeserializeObject<List<ConsumableEquipment>>(json);
            }
            else
            {
                consumableEquipment = new List<ConsumableEquipment>();
                WriteInFile(consumableEquipment);
            }
            return consumableEquipment;
        }

        private void WriteInFile(List<ConsumableEquipment> consumableEquipment)
        {
            string json = JsonConvert.SerializeObject(consumableEquipment);
            File.WriteAllText(path, json);
        }



    }
}