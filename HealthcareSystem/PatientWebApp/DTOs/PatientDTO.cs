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
        public BloodType BloodType { get; set; }
        public RhFactorType RhFactor { get; set; }
        public bool HasInsurance { get; set; }
        public string Lbo { get; set; }
        public string Alergies { get; set; }
        public string MedicalHistory { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public PatientDTO() { }
    }
}
