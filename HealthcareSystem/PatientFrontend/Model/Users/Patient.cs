/***********************************************************************
 * Module:  Patient.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Patient
 ***********************************************************************/

using ZdravoKorporacija;
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
            this.Jmbg = jmbg;
            this.Name = name;
            this.Surname = surname;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.City = city;
            this.HomeAddress = homeAddress;
            this.Phone = phone;
            this.Email = email;
            this.Username = username;
            this.Password = password;
            this.DateOfRegistration = dateOfRegistration;
            this.IsGuest = isGuest;
        }
        public Patient(Patient patient)
        {
            this.Jmbg = patient.Jmbg;
            this.Name = patient.Name;
            this.Surname = patient.Surname;
            this.DateOfBirth = patient.DateOfBirth;
            this.Gender = patient.Gender;
            this.City = patient.City;
            this.HomeAddress = patient.HomeAddress;
            this.Phone = patient.Phone;
            this.Email = patient.Email;
            this.Username = patient.Username;
            this.Password = patient.Password;
            this.DateOfRegistration = patient.DateOfRegistration;
            this.IsGuest = patient.IsGuest;
        }
    }
}