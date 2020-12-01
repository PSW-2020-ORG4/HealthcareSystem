/***********************************************************************
 * Module:  SecretaryRepository.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Repository.SecretaryRepository
 ***********************************************************************/

using Model.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   public class SecretaryRepository
   {
        private string path;

        public SecretaryRepository()
        {
            string fileName = "secretary.json";
            path = Path.GetFullPath(fileName);
        }

        public Model.Users.Secretary GetSecretary(string jmbg)
        {
            List<Secretary> secretaries = ReadFromFile();
            foreach (Secretary s in secretaries)
            {
                if (s.Jmbg.Equals(jmbg))
                {
                    return s;
                }
            }
            return null;
        }

        public List<Secretary> GetAllSecretaries()
        {
            List<Secretary> secretaries = ReadFromFile();
            return secretaries;
        }
      
        public Model.Users.Secretary SetSecretary(Model.Users.Secretary secretary)
        {
            List<Secretary> secretaries = ReadFromFile();
            foreach(Secretary s in secretaries)
            {
                if (s.Jmbg.Equals(secretary.Jmbg))
                {
                    s.Name = secretary.Name;
                    s.Surname = secretary.Surname;
                    s.Gender = secretary.Gender;
                    s.DateOfBirth = secretary.DateOfBirth;
                    s.City = secretary.City;
                    s.Phone = secretary.Phone;
                    s.Email = secretary.Email;
                    s.HomeAddress = secretary.HomeAddress;
                    s.Username = secretary.Username;
                    s.Password = secretary.Password;
                    s.Qualifications = secretary.Qualifications;
                    s.DateOfEmployment = secretary.DateOfEmployment;
                    break;
                }
            }
            WriteInFile(secretaries);
            return secretary;
        }
      
        public Model.Users.Secretary NewSecretary(Model.Users.Secretary secretary)
        {
            List<Secretary> secretaries = ReadFromFile();
            Secretary searchSecretary = GetSecretary(secretary.Jmbg);
            if(searchSecretary != null)
            {
                return null;
            }
            secretaries.Add(secretary);
            WriteInFile(secretaries);
            return secretary;
        }
      
        public bool DeleteSecretary(string jmbg)
        {
            List<Secretary> secretaries = ReadFromFile();
            Secretary secretaryForDelete = null;
            foreach (Secretary s in secretaries)
            {
                if (s.Jmbg.Equals(jmbg))
                {
                    secretaryForDelete = s;
                    break;
                }
            }
            if (secretaryForDelete == null)
                return false;
            secretaries.Remove(secretaryForDelete);
            WriteInFile(secretaries);
            return true;
        }

        public Secretary CheckUsernameAndPassword(string username, string password)
        {
            List<Secretary> secretaries = ReadFromFile();
            foreach (Secretary s in secretaries)
            {
                if (s.Username.Equals(username) && s.Password.Equals(password))
                {
                    return s;
                }
            }
            return null;
        }

        private List<Secretary> ReadFromFile()
        {
            List<Secretary> secretaries;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                secretaries = JsonConvert.DeserializeObject<List<Secretary>>(json);
            }
            else
            {
                secretaries = new List<Secretary>();
                WriteInFile(secretaries);
            }
            return secretaries;
        }
      
        private void WriteInFile(List<Secretary> secretaries)
        {
            string json = JsonConvert.SerializeObject(secretaries);
            File.WriteAllText(path, json);
        }

    }
}