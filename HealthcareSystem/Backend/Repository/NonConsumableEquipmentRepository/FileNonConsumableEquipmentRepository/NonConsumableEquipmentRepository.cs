/***********************************************************************
 * Module:  NonConsumableEquipmentRepository.cs
 * Author:  Jelena Budisa
 * Purpose: Definition of the Class Repository.NonConsumableEquipmentRepository
 ***********************************************************************/

using System;
using Model.Manager;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Backend.Repository;

namespace Repository
{
    public class NonConsumableEquipmentRepository : INonConsumableEquipmentRepository
    {
        private string path;

        public NonConsumableEquipmentRepository()
        {

            string fileName = "nonConsumableEquipment.json";
            path = Path.GetFullPath(fileName);
        }

        public Model.Manager.NonConsumableEquipment GetEquipment(int id)
        {
            List<NonConsumableEquipment> nonConsumableEquipment = ReadFromFile();
            foreach (NonConsumableEquipment e in nonConsumableEquipment)
            {
                if (e.Id.Equals(id))
                {
                    return e;
                }
            }

            return null;
        }

        public List<NonConsumableEquipment> GetAllEquipment()
        {
            List<NonConsumableEquipment> nonConsumableEquipment = ReadFromFile();
            return nonConsumableEquipment;
        }

        public Model.Manager.NonConsumableEquipment SetEquipment(Model.Manager.NonConsumableEquipment equipment)
        {
            List<NonConsumableEquipment> nonConsumableEquipment = ReadFromFile();

            foreach (NonConsumableEquipment e in nonConsumableEquipment)
            {
                if (e.Id.Equals(equipment.Id))
                {
                    
                    e.Type = equipment.Type;
                    break;
                }
            }
            WriteInFile(nonConsumableEquipment);
            return equipment;
        }

        public bool DeleteEquipment(int id)
        {
            List<NonConsumableEquipment> nonConsumableEquipment = ReadFromFile();
            NonConsumableEquipment equipmentForDelete = null;
            foreach (NonConsumableEquipment e in nonConsumableEquipment)
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

            nonConsumableEquipment.Remove(equipmentForDelete);
            WriteInFile(nonConsumableEquipment);
            return true;
        }

        public Model.Manager.NonConsumableEquipment NewEquipment(Model.Manager.NonConsumableEquipment equipment)
        {
            List<NonConsumableEquipment> nonConsumableEquipment = ReadFromFile();
            NonConsumableEquipment searchEquipment = GetEquipment(equipment.Id);
            if (searchEquipment != null)
            {
                return null;
            }

            nonConsumableEquipment.Add(equipment);
            WriteInFile(nonConsumableEquipment);
            return equipment;
        }

        private List<NonConsumableEquipment> ReadFromFile()
        {
            List<NonConsumableEquipment> nonConsumableEquipment;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                nonConsumableEquipment = JsonConvert.DeserializeObject<List<NonConsumableEquipment>>(json);
            }
            else
            {
                nonConsumableEquipment = new List<NonConsumableEquipment>();
                WriteInFile(nonConsumableEquipment);
            }
            return nonConsumableEquipment;
        }

        private void WriteInFile(List<NonConsumableEquipment> nonConsumableEquipment)
        {
            string json = JsonConvert.SerializeObject(nonConsumableEquipment);
            File.WriteAllText(path, json);
        }




    }
}