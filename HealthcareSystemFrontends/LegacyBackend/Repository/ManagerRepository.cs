/***********************************************************************
 * Module:  ManagerRepository.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Repository.ManagerRepository
 ***********************************************************************/

using Model.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class ManagerRepository
   {
        private string path;
        public ManagerRepository()
        {
            string fileName = "manager.json";
            path = Path.GetFullPath(fileName);
        }

        public List<Manager> GetAllManagers()
        {
            List<Manager> managers = ReadFromFile();
            return managers;
        }
        public Model.Users.Manager GetManager(string jmbg)
        {
            List<Manager> managers = ReadFromFile();
            foreach(Manager m in managers)
            {
                if (m.Jmbg.Equals(jmbg))
                {
                    return m;
                }
            }
            return null;
        }
       
      public Model.Users.Manager SetManager(Model.Users.Manager manager)
      {
            List<Manager> managers = ReadFromFile();
            foreach (Manager m in managers)
            {
                if (m.Jmbg.Equals(manager.Jmbg))
                {
                    m.Name = manager.Name;
                    m.Surname = manager.Surname;
                    m.Gender = manager.Gender;
                    m.DateOfBirth = manager.DateOfBirth;
                    m.City = manager.City;
                    m.Phone = manager.Phone;
                    m.Email = manager.Email;
                    m.HomeAddress = manager.HomeAddress;
                    m.Username = manager.Username;
                    m.Password = manager.Password;
                    m.Qualifications = manager.Qualifications;
                    m.DateOfEmployment = manager.DateOfEmployment;
                    break;
                }
            }
            WriteInFile(managers);
            return manager;
        }

        public Manager CheckUsernameAndPassword(string username, string password)
        {
            List<Manager> managers = ReadFromFile();
            foreach (Manager m in managers)
            {
                if (m.Username.Equals(username) && m.Password.Equals(password))
                {
                    return m;
                }
            }
            return null;
        }

        private List<Manager> ReadFromFile()
        {
            List<Manager> managers;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                managers = JsonConvert.DeserializeObject<List<Manager>>(json);
            }
            else
            {
                managers = new List<Manager>();
                WriteInFile(managers);
            }
            return managers;
        }

        private void WriteInFile(List<Manager> managers)
        {
            string json = JsonConvert.SerializeObject(managers);
            File.WriteAllText(path, json);
        }

    }
}