/***********************************************************************
 * Module:  Manager.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Users.Manager
 ***********************************************************************/

using System;

namespace Model.Users
{
   public class Manager : User
   {
        public TypeOfQualifications Qualifications { get; set; }
        public DateTime DateOfEmployment { get; set; }

        public Manager() { }

        public Manager(string jmbg, string name, string surname, DateTime dateOfBirth, GenderType gender, City city, string homeAddress, string phone,
                         string email, string username, string password, TypeOfQualifications qualifications, DateTime dateOfEmployment)
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
            this.Qualifications = qualifications;
            this.DateOfEmployment = dateOfEmployment;
        }

        public Manager(Manager manager)
        {
            this.Jmbg = manager.Jmbg;
            this.Name = manager.Name;
            this.Surname = manager.Surname;
            this.DateOfBirth = manager.DateOfBirth;
            this.Gender = manager.Gender;
            this.City = manager.City;
            this.HomeAddress = manager.HomeAddress;
            this.Phone = manager.Phone;
            this.Email = manager.Email;
            this.Username = manager.Username;
            this.Password = manager.Password;
            this.Qualifications = manager.Qualifications;
            this.DateOfEmployment = manager.DateOfEmployment;
        }
   
   }
}