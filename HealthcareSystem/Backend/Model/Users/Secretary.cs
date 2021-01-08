/***********************************************************************
 * Module:  Secretary.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Secretary
 ***********************************************************************/



using Backend.Model.Enums;
using System;

namespace Model.Users
{
    public class Secretary : User
   {
        public TypeOfQualifications Qualifications { get; set; }
        public DateTime DateOfEmployment { get; set; }

        public Secretary() { }

        public Secretary(string jmbg,string name,string surname,DateTime dateOfBirth,GenderType gender,City city,string homeAddress,string phone,
                         string email,string username,string password,TypeOfQualifications qualifications,DateTime dateOfEmployment)
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
            Qualifications = qualifications;
            DateOfEmployment = dateOfEmployment;
        }
        public Secretary(Secretary secretary)
        {
            Jmbg = secretary.Jmbg;
            Name = secretary.Name;
            Surname = secretary.Surname;
            DateOfBirth = secretary.DateOfBirth;
            Gender = secretary.Gender;
            if (secretary.City != null)
            {
                City = new City(secretary.City);
                CityZipCode = secretary.City.ZipCode;
            }
            else
            {
                City = new City();
                CityZipCode = 0;
            }
            HomeAddress = secretary.HomeAddress;
            Phone = secretary.Phone;
            Email = secretary.Email;
            Username = secretary.Username;
            Password = secretary.Password;
            Qualifications = secretary.Qualifications;
            DateOfEmployment = secretary.DateOfEmployment;
        }

    }
}