/***********************************************************************
 * Module:  Doctor.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Users.Doctor
 ***********************************************************************/

using Model.Manager;
using System;

namespace Model.Users
{
   public class Doctor : User
   {
        public string NumberOfLicence { get; set; }
        public TypeOfDoctor Type { get; set; }
        public Room DoctorsOffice { get; set; }
        public DateTime DateOfEmployment { get; set; }

        public Doctor() { }

        public Doctor(string jmbg, string name, string surname, DateTime dateOfBirth, GenderType gender, City city, string homeAddress, string phone,
                         string email, string username, string password,string numberOfLicence,TypeOfDoctor typeOfDoctor,Room doctorsOffice,DateTime dateOfEmployment)
        {
            this.Jmbg = jmbg;
            this.Name = name;
            this.Surname = surname;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            if(city != null)
            {
                this.City = new City(city);
            }
            else
            {
                this.City = new City();
            }
            this.HomeAddress = homeAddress;
            this.Phone = phone;
            this.Email = email;
            this.Username = username;
            this.Password = password;
            this.NumberOfLicence = numberOfLicence;
            this.Type = typeOfDoctor;
            if (doctorsOffice != null)
            {
                this.DoctorsOffice = new Room(doctorsOffice);
            }
            else
            {
                this.DoctorsOffice = new Room();
            }
            this.DateOfEmployment = dateOfEmployment;
        }
        public Doctor(Doctor doctor)
        {
            this.Jmbg = doctor.Jmbg;
            this.Name = doctor.Name;
            this.Surname = doctor.Surname;
            this.DateOfBirth = doctor.DateOfBirth;
            this.Gender = doctor.Gender;
            if (doctor.City != null)
            {
                this.City = new City(doctor.City);
            }
            else
            {
                this.City = new City();
            }
            this.HomeAddress = doctor.HomeAddress;
            this.Phone = doctor.Phone;
            this.Email = doctor.Email;
            this.Username = doctor.Username;
            this.Password = doctor.Password;
            this.NumberOfLicence = doctor.NumberOfLicence;
            this.Type = doctor.Type;
            if (doctor.DoctorsOffice != null)
            {
                this.DoctorsOffice = new Room(doctor.DoctorsOffice);
            }
            else
            {
                this.DoctorsOffice = new Room();
            }
            this.DateOfEmployment = doctor.DateOfEmployment;
        } 

    }
}