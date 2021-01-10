using Backend.Model.Enums;
using System;

namespace PatientWebApp.DTOs
{
    public class PatientDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Jmbg { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CityZipCode { get; set; }
        public string CityName { get; set; }
        public string HomeAddress { get; set; }
        public int PatientCardId { get; set; }
        public int BloodType { get; set; }
        public int RhFactor { get; set; }
        public string Lbo { get; set; }
        public string Allergies { get; set; }
        public string MedicalHistory { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
