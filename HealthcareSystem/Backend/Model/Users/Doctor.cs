using Backend.Model;
using Backend.Model.Users;
using Model.Enums;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users
{
   public class Doctor : User
   {
        public string NumberOfLicence { get; set; }

        [ForeignKey("Specialty")]
        public int SpecialtyId { get; set; }
        public virtual Specialty Specialty { get; set; }
        public DateTime DateOfEmployment { get; set; }

        [ForeignKey("DoctorsOffice")]
        public int DoctorsOfficeId { get; set; }
        public virtual Room DoctorsOffice { get; set; }
        public virtual ICollection<SurveyAboutDoctor> SurveysAboutDoctor { get; set; }
        public virtual ICollection<WorkingTime> WorkingTimes { get; set; }

        public Doctor() { }

        public Doctor(string jmbg, string name, string surname, DateTime dateOfBirth, GenderType gender, City city, string homeAddress, string phone,
                         string email, string username, string password, string numberOfLicence, Specialty specialty, Room doctorsOffice, DateTime dateOfEmployment)
        {
            Jmbg = jmbg;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            if (city != null)
            {
                City = new City(city);
                CityZipCode = city.ZipCode;
            }
            else
            {
                City = new City();
                CityZipCode = 0;
            }
            HomeAddress = homeAddress;
            Phone = phone;
            Email = email;
            Username = username;
            Password = password;
            NumberOfLicence = numberOfLicence;
            if (specialty != null)
            {
                Specialty = new Specialty(specialty);
            }
            else
            {
                Specialty = new Specialty();
            }
            if (doctorsOffice != null)
            {
                DoctorsOffice = new Room(doctorsOffice);
            }
            else
            {
                DoctorsOffice = new Room();
            }
            DateOfEmployment = dateOfEmployment;
        }


        public Doctor(Doctor doctor)
        {
            Jmbg = doctor.Jmbg;
            Name = doctor.Name;
            Surname = doctor.Surname;
            DateOfBirth = doctor.DateOfBirth;
            Gender = doctor.Gender;
            if (doctor.City != null)
            {
                City = new City(doctor.City);
                CityZipCode = doctor.City.ZipCode;
            }
            else
            {
                City = new City();
                CityZipCode = 0;
            }
            HomeAddress = doctor.HomeAddress;
            Phone = doctor.Phone;
            Email = doctor.Email;
            Username = doctor.Username;
            Password = doctor.Password;
            NumberOfLicence = doctor.NumberOfLicence;
            if (doctor.Specialty != null)
            {
                Specialty = new Specialty(doctor.Specialty);
            }
            else
            {
                Specialty = new Specialty();
            }
            if (doctor.DoctorsOffice != null)
            {
                DoctorsOffice = new Room(doctor.DoctorsOffice);
            }
            else
            {
                DoctorsOffice = new Room();
            }
            DateOfEmployment = doctor.DateOfEmployment;
        }

    }
}