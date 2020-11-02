/***********************************************************************
 * Module:  Patient.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Patient
 ***********************************************************************/

using System;

namespace Model.Users
{
   public class Patient : User
   {
        public DateTime DateOfRegistration { get; set; }
        public bool IsGuest { get; set; }

        public Patient()
        {
            IsGuest = false;
        }

        public Patient(string jmbg,string name,string surname,DateTime dateOfBirth,GenderType gender,City city,string homeAddress,string phone,string email,string username,
                        string password,DateTime dateOfRegistration,bool isGuest)
        {
            Jmbg = jmbg;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            if (city != null) { City = new City(city); }
            else { City = new City(); }
            HomeAddress = homeAddress;
            Phone = phone;
            Email = email;
            Username = username;
            Password = password;
            DateOfRegistration = dateOfRegistration;
            IsGuest = isGuest;
        }
        public Patient(Patient patient)
        {
            Jmbg = patient.Jmbg;
            Name = patient.Name;
            Surname = patient.Surname;
            DateOfBirth = patient.DateOfBirth;
            Gender = patient.Gender;
            if (patient.City != null) { City = new City(patient.City); }
            else { City = new City(); }
            HomeAddress = patient.HomeAddress;
            Phone = patient.Phone;
            Email = patient.Email;
            Username = patient.Username;
            Password = patient.Password;
            DateOfRegistration = patient.DateOfRegistration;
            IsGuest = patient.IsGuest;
        }
    }
}