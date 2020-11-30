/***********************************************************************
 * Module:  DoctorRepository.cs
 * Author:  Dragana Carapic
 * Purpose: Definition of the Class Repository.DoctorRepository
 ***********************************************************************/

using Model.Users;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System;

namespace Repository
{
    public class DoctorRepository
   {
        private string path;

        public DoctorRepository()
        {
            string fileName = "doctor.json";
            path = Path.GetFullPath(fileName);
             
        }
        public Model.Users.Doctor GetDoctor(string jmbg)
        {
            // TODO: implement
            List<Doctor> doctorList = ReadFromFile();
            foreach (Doctor d in doctorList)
            {
                if (d.Jmbg.Equals(jmbg))
                {
                    return d;
                }
            }
            return null;
        }

        public Model.Users.Doctor SetDoctor(Model.Users.Doctor doctor)
        {
            // TODO: implement
            List<Doctor> doctorList = ReadFromFile();
            foreach (Doctor d in doctorList)
            {
                if (d.Jmbg.Equals(doctor.Jmbg))
                {
                    d.Name = doctor.Name;
                    d.Surname = doctor.Surname;
                    d.Gender = doctor.Gender;
                    d.DateOfBirth = doctor.DateOfBirth;
                    d.City = doctor.City;
                    d.Phone = doctor.Phone;
                    d.Email = doctor.Email;
                    d.HomeAddress = doctor.HomeAddress;
                    d.Username = doctor.Username;
                    d.Password = doctor.Password;
                    d.NumberOfLicence = doctor.NumberOfLicence;
                    d.Type = doctor.Type;
                    d.DoctorsOffice = doctor.DoctorsOffice;
                    d.DateOfEmployment = doctor.DateOfEmployment;

                    break;
                }
            }
            WriteInFile(doctorList);
            return null;
        }

        public List<Doctor> GetAllDoctors()
        {
            // TODO: implement
            List<Doctor> doctorList = ReadFromFile();
           
            return doctorList;
            
        }

        public Model.Users.Doctor NewDoctor(Model.Users.Doctor doctor)
        {
            // TODO: implement
            List<Doctor> doctorList = ReadFromFile();
            Doctor searchDoctor = GetDoctor(doctor.Jmbg);
            if (searchDoctor != null)
            {
                return null;
            }
     
            doctorList.Add(doctor);
            WriteInFile(doctorList);

            return doctor;

        }

        public bool DeleteDoctor(string jmbg)
        {
            // TODO: implement
            List<Doctor> doctorList = ReadFromFile();
            Doctor doctorForDelete = null;
            
            foreach (Doctor d in doctorList)
            {
                if (d.Jmbg.Equals(jmbg))
                {
                        doctorForDelete = d;
                        break;
                }
            }
            if (doctorForDelete == null) {
                return false;
            }

            doctorList.Remove(doctorForDelete);
            WriteInFile(doctorList);
            return true;
            
            

        }

        private List<Doctor> ReadFromFile()
        {
            List<Doctor> doctorList;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                doctorList = JsonConvert.DeserializeObject<List<Doctor>>(json);
            }
            else
            {
                doctorList = new List<Doctor>();
                WriteInFile(doctorList);
            }

            return doctorList;

        }

        private void WriteInFile(List<Doctor> doctorList)
        {
            string json = JsonConvert.SerializeObject(doctorList);
            File.WriteAllText(path, json);

        }

        public Doctor CheckUsernameAndPassword(string username, string password)
        {
            List<Doctor> doctorList = ReadFromFile();
            foreach (Doctor d in doctorList)
            {
                if (d.Username.Equals(username) && d.Password.Equals(password))
                {
                    return d;
                }
            }
            return null;
        }
    }
}