using Model.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class FileDoctorRepository : IDoctorRepository
    {
        private string _path;

        public FileDoctorRepository()
        {
            string fileName = "doctor.json";
            _path = Path.GetFullPath(fileName);
        }

        public void AddDoctor(Doctor doctor)
        {
            List<Doctor> doctors = ReadFromFile();
            Doctor searchDoctor = GetDoctorByJmbg(doctor.Jmbg);
            if (searchDoctor != null) { throw new ValidationException(); }
            doctors.Add(doctor);
            WriteInFile(doctors);
        }

        public Doctor CheckUsernameAndPassword(string username, string password)
        {
            List<Doctor> doctors = ReadFromFile();
            foreach (Doctor d in doctors)
            {
                if (d.Username.Equals(username) && d.Password.Equals(password)) { return d; }
            }
            return null;
        }

        public List<Doctor> GetAllDoctors()
        {
            List<Doctor> doctors = ReadFromFile();
            return doctors;
        }

        public Doctor GetDoctorByJmbg(string jmbg)
        {
            List<Doctor> doctors = ReadFromFile();
            foreach (Doctor d in doctors)
            {
                if (d.Jmbg.Equals(jmbg)) { return d; }
            }
            return null;
        }

        public void SetDoctor(Doctor doctor)
        {
            List<Doctor> doctors = ReadFromFile();
            foreach (Doctor d in doctors)
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
                    d.Specialty = doctor.Specialty;
                    d.DoctorsOffice = doctor.DoctorsOffice;
                    d.DateOfEmployment = doctor.DateOfEmployment;

                    break;
                }
            }
            WriteInFile(doctors);
        }

        private List<Doctor> ReadFromFile()
        {
            List<Doctor> doctors;
            if (File.Exists(_path))
            {
                string json = File.ReadAllText(_path);
                doctors = JsonConvert.DeserializeObject<List<Doctor>>(json);
            }
            else
            {
                doctors = new List<Doctor>();
                WriteInFile(doctors);
            }
            return doctors;
        }

        private void WriteInFile(List<Doctor> doctors)
        {
            string json = JsonConvert.SerializeObject(doctors);
            File.WriteAllText(_path, json);
        }
    }
}
