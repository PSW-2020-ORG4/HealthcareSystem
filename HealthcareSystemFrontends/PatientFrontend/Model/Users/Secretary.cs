/***********************************************************************
 * Module:  Secretary.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Secretary
 ***********************************************************************/

using ZdravoKorporacija;
using Syncfusion.Windows.Shared;
using Syncfusion.XPS;
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
        public Secretary(Secretary secretary)
        {
            this.Jmbg = secretary.Jmbg;
            this.Name = secretary.Name;
            this.Surname = secretary.Surname;
            this.DateOfBirth = secretary.DateOfBirth;
            this.Gender = secretary.Gender;
            this.City = secretary.City;
            this.HomeAddress = secretary.HomeAddress;
            this.Phone = secretary.Phone;
            this.Email = secretary.Email;
            this.Username = secretary.Username;
            this.Password = secretary.Password;
            this.Qualifications = secretary.Qualifications;
            this.DateOfEmployment = secretary.DateOfEmployment;
        }

    }
}