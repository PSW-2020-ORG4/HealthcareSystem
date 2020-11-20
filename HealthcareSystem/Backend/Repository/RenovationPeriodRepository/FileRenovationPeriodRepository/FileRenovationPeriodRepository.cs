/***********************************************************************
 * Module:  RenovationPeriodRepository.cs
 * Author:  LukaRA252017
 * Purpose: Definition of the Class Repository.RenovationPeriodRepository
 ***********************************************************************/


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using Backend.Repository.RenovationPeriodRepository;
using Model;
using Model.Manager;
using Newtonsoft.Json;

namespace Repository
{
    public class RenovationPeriodRepository : IRenovationPeriodRepository
    {
        private string path;

        public RenovationPeriodRepository()
        {
            string fileName = "renovationperiods.json";
            path = Path.GetFullPath(fileName);
        }
        public List<RenovationPeriod> GetAllRenovationPeriod()
        {
            List<RenovationPeriod> renovationPeriods = ReadFromFile();
            return renovationPeriods;
        }

        public Model.Manager.RenovationPeriod GetRenovationPeriodByRoomNumber(int roomNumber)
        {
            List<RenovationPeriod> renovationPeriods = ReadFromFile();

            foreach (RenovationPeriod renovationPeriod in renovationPeriods)
            {
                if (renovationPeriod.RoomNumber == roomNumber)
                {
                    return renovationPeriod;
                }
            }

            return null;
        }
        public void UpdateRenovationPeriod(Model.Manager.RenovationPeriod renovationPeriod)
        {
            List<RenovationPeriod> renovationPeriods = ReadFromFile();

            foreach (RenovationPeriod rp in renovationPeriods)
            {
                if (rp.RoomNumber == renovationPeriod.RoomNumber)
                {
                    rp.RoomNumber = renovationPeriod.RoomNumber;
                    rp.BeginDate = renovationPeriod.BeginDate;
                    rp.EndDate = renovationPeriod.EndDate;
                    break;
                }
            }

            WriteInFile(renovationPeriods);
        }

        public void DeleteRenovationPeriod(int roomNumber)
        {
            List<RenovationPeriod> renovationPeriods = ReadFromFile();
            RenovationPeriod renovationPeriodForDelete = null;

            foreach (RenovationPeriod renovationPeriod in renovationPeriods)
            {
                if (renovationPeriod.RoomNumber == roomNumber)
                {
                    renovationPeriodForDelete = renovationPeriod;
                    break;
                }
            }
            if (renovationPeriodForDelete == null)
                throw new ValidationException();
            renovationPeriods.Remove(renovationPeriodForDelete);
            WriteInFile(renovationPeriods);
        }

        public void AddRenovationPeriod(Model.Manager.RenovationPeriod renovationPeriod)
        {
            List<RenovationPeriod> renovationPeriods = ReadFromFile();
            RenovationPeriod searchRenovationPeriod = GetRenovationPeriodByRoomNumber(renovationPeriod.RoomNumber);
            if (searchRenovationPeriod != null)
            {
                throw new ValidationException();
            }
            renovationPeriods.Add(renovationPeriod);
            WriteInFile(renovationPeriods);
        }

        private List<RenovationPeriod> ReadFromFile()
        {
            List<RenovationPeriod> renovationPeriods;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                renovationPeriods = JsonConvert.DeserializeObject<List<RenovationPeriod>>(json);
            }
            else
            {
                renovationPeriods = new List<RenovationPeriod>();
                WriteInFile(renovationPeriods);
            }

            return renovationPeriods;
        }

        private void WriteInFile(List<RenovationPeriod> renovationPeriods)
        {
            string json = JsonConvert.SerializeObject(renovationPeriods);
            File.WriteAllText(path, json);
        }

    }
}
