﻿using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model.Users
{
    class Admin : User
    {
        public Admin() { }
        public Admin(string jmbg, string name, string surname, DateTime dateOfBirth, GenderType gender, City city, string homeAddress, string phone, string email, string username,
                        string password)
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
        }

        public Admin(Admin admin)
        {
            Jmbg = admin.Jmbg;
            Name = admin.Name;
            Surname = admin.Surname;
            DateOfBirth = admin.DateOfBirth;
            Gender = admin.Gender;
            if (admin.City != null) { City = new City(admin.City); }
            else { City = new City(); }
            HomeAddress = admin.HomeAddress;
            Phone = admin.Phone;
            Email = admin.Email;
            Username = admin.Username;
            Password = admin.Password;
        }
    }
}
