/***********************************************************************
 * Module:  RenovationPeriodRepository.cs
 * Author:  LukaRA252017
 * Purpose: Definition of the Class Repository.RenovationPeriodRepository
 ***********************************************************************/


using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Model.Manager;
using Newtonsoft.Json;

namespace Repository
{
    public class RenovationPeriodRepository
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
                if (renovationPeriod.room.Number == roomNumber)
                {
                    return renovationPeriod;
                }
            }

            return null;
        }
        public Model.Manager.RenovationPeriod SetRenovationPeriod(Model.Manager.RenovationPeriod renovationPeriod)
        {
            List<RenovationPeriod> renovationPeriods = ReadFromFile();

            foreach (RenovationPeriod rp in renovationPeriods)
            {
                if (rp.room.Number == renovationPeriod.room.Number)
                {
                    rp.room = renovationPeriod.room;
                    rp.BeginDate = renovationPeriod.BeginDate;
                    rp.EndDate = renovationPeriod.EndDate;
                    break;
                }
            }

            WriteInFile(renovationPeriods);
            return renovationPeriod;
        }

        public bool DeleteRenovationPeriod(int roomNumber)
        {
            List<RenovationPeriod> renovationPeriods = ReadFromFile();
            RenovationPeriod renovationPeriodForDelete = null;
            
            foreach (RenovationPeriod renovationPeriod in renovationPeriods)
            {
                if (renovationPeriod.room.Number == roomNumber)
                {
                    renovationPeriodForDelete = renovationPeriod;
                    break;
                }
            }
            if (renovationPeriodForDelete == null)
                return false;

            renovationPeriods.Remove(renovationPeriodForDelete);
            WriteInFile(renovationPeriods);
            return true;
        }

        public Model.Manager.RenovationPeriod NewRenovationPeriod(Model.Manager.RenovationPeriod renovationPeriod)
        {
            List<RenovationPeriod> renovationPeriods = ReadFromFile();
            RenovationPeriod searchRenovationPeriod = GetRenovationPeriodByRoomNumber(renovationPeriod.room.Number);

            if (searchRenovationPeriod != null)
                return null;

            renovationPeriods.Add(renovationPeriod);
            WriteInFile(renovationPeriods);
            return renovationPeriod;
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
