/***********************************************************************
 * Module:  Patient.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Patient
 ***********************************************************************/

using Backend.Model.Enums;
using Model.NotificationSurveyAndFeedback;
using Model.Users;
using System;
using System.Collections.Generic;

namespace Backend.Model.Users
{
    public class Patient : User
    {
        public DateTime DateOfRegistration { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsActive { get; set; }
        public bool IsGuest { get; set; }
        public string ImageName { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual PatientCard PatientCard { get; set; }
        public Patient()
        {
            IsBlocked = false;
        }

        public Patient(string jmbg, string name, string surname, DateTime dateOfBirth, GenderType gender, City city, string homeAddress,
                        string phone, string email, string username, string password,
                        DateTime dateOfRegistration, bool isBlocked, string imagePath)
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
            DateOfRegistration = dateOfRegistration;
            IsBlocked = isBlocked;
            IsActive = false;
            ImageName = imagePath;
        }

        public Patient(Patient patient)
        {
            Jmbg = patient.Jmbg;
            Name = patient.Name;
            Surname = patient.Surname;
            DateOfBirth = patient.DateOfBirth;
            Gender = patient.Gender;
            if (patient.City != null)
            {
                City = new City(patient.City);
                CityZipCode = patient.City.ZipCode;
            }
            else
            {
                City = new City();
                CityZipCode = 0;
            }
            HomeAddress = patient.HomeAddress;
            Phone = patient.Phone;
            Email = patient.Email;
            Username = patient.Username;
            Password = patient.Password;
            DateOfRegistration = patient.DateOfRegistration;
            IsBlocked = patient.IsBlocked;
            IsActive = false;
            ImageName = patient.ImageName;
        }

    }
}