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
            Qualifications = qualifications;
            DateOfEmployment = dateOfEmployment;
        }

        public Manager(Manager manager)
        {

            Jmbg = manager.Jmbg;
            Name = manager.Name;
            Surname = manager.Surname;
            DateOfBirth = manager.DateOfBirth;
            Gender = manager.Gender;
            if (manager.City != null)
            {
                City = new City(manager.City);
            }
            else
            {
                City = new City();
            }
            CityZipCode = manager.City.ZipCode;
            HomeAddress = manager.HomeAddress;
            Phone = manager.Phone;
            Email = manager.Email;
            Username = manager.Username;
            Password = manager.Password;
            Qualifications = manager.Qualifications;
            DateOfEmployment = manager.DateOfEmployment;
        }
   
   }
}