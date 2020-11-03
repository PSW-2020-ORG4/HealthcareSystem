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
            Jmbg = jmbg;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            if (city != null)
            {
                City = new City(city);
            }
            else
            {
                City = new City();
            }
            CityZipCode = city.ZipCode;
            HomeAddress = homeAddress;
            Phone = phone;
            Email = email;
            Username = username;
            Password = password;
            NumberOfLicence = numberOfLicence;
            Type = typeOfDoctor;
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
            }
            else
            {
                City = new City();
            }
            CityZipCode = doctor.City.ZipCode;
            HomeAddress = doctor.HomeAddress;
            Phone = doctor.Phone;
            Email = doctor.Email;
            Username = doctor.Username;
            Password = doctor.Password;
            NumberOfLicence = doctor.NumberOfLicence;
            Type = doctor.Type;
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