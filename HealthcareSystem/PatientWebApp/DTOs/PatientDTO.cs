using Backend.Model.Enums;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.DTOs
{
    public class PatientDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Jmbg { get; set; }
        public GenderType Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CityZipCode { get; set; }
        public string CityName { get; set; }
        public string HomeAddress { get; set; }
        public int PatientCardId { get; set; }
        public BloodType BloodType { get; set; }
        public RhFactorType RhFactor { get; set; }
        public bool HasInsurance { get; set; }
        public string Lbo { get; set; }
        public string Allergies { get; set; }
        public string MedicalHistory { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImageName { get; set; }
        public bool IsBlocked { get; set; }

        public PatientDTO() { }

        public PatientDTO(string name, string surname, string jmbg, GenderType gender, DateTime dateOfBirth, 
                          string phone, int countryId, string countryName, int cityZipCode, string cityName, 
                          string homeAddress, BloodType bloodType, RhFactorType rhFactor, bool hasInsurance, 
                          string lbo, string alergies, string medicalHistory, string email, string password,
                          bool isBlocked)
        {
            Name = name;
            Surname = surname;
            Jmbg = jmbg;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Phone = phone;
            CountryId = countryId;
            CountryName = countryName;
            CityZipCode = cityZipCode;
            CityName = cityName;
            HomeAddress = homeAddress;
            BloodType = bloodType;
            RhFactor = rhFactor;
            HasInsurance = hasInsurance;
            Lbo = lbo;
            Allergies = alergies;
            MedicalHistory = medicalHistory;
            Email = email;
            Password = password;
            IsBlocked = isBlocked;
        }
    }
}
