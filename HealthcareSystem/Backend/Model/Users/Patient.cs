/***********************************************************************
 * Module:  Patient.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Patient
 ***********************************************************************/

using Model.Enums;
using Model.NotificationSurveyAndFeedback;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users
{
   public class Patient : User
   {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateOfRegistration { get; set; }
        public bool IsGuest { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual PatientCard PatientCard { get; set; }
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
            IsGuest = isGuest;
            IsActive = false;
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
            IsGuest = patient.IsGuest;
            IsActive = false;
        }
    }
}