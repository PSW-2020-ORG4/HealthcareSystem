using Model.Users;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.Adapters
{
    public class PatientMapper
    {
        /// <summary>
        /// /mapping objects Patient and PatientCard to type PatientDTO
        /// </summary>
        /// <param name="patient">object type Patient</param>
        /// <param name="patientCard">object type PatientCard</param>
        /// <returns>object type PatientDTO</returns>
        public static PatientDTO PatientAndPatientCardToPatientDTO(Patient patient, PatientCard patientCard)
        {
            PatientDTO patientDto = new PatientDTO();

            patientDto.Jmbg = patient.Jmbg;
            patientDto.Name = patient.Name;
            patientDto.Surname = patient.Surname;
            patientDto.PatientCardId = patientCard.Id; 
            patientDto.Gender = patient.Gender;
            patientDto.DateOfBirth = patient.DateOfBirth;
            patientDto.Phone = patient.Phone;
            patientDto.CountryId = patient.City.CountryId;
            patientDto.CountryName = patient.City.Country.Name;
            patientDto.CityZipCode = patient.CityZipCode;
            patientDto.CityName = patient.City.Name;
            patientDto.HomeAddress = patient.HomeAddress;
            patientDto.Email = patient.Email;
            patientDto.Password = patient.Password;

            patientDto.BloodType = patientCard.BloodType;
            patientDto.RhFactor = patientCard.RhFactor;
            patientDto.HasInsurance = patientCard.HasInsurance;
            patientDto.Lbo = patientCard.Lbo;
            patientDto.Alergies = patientCard.Alergies;
            patientDto.MedicalHistory = patientCard.MedicalHistory;

            return patientDto;
        }

        /// <summary>
        /// /mapping object type PatientDTO to type Patient
        /// </summary>
        /// <param name="patientDTO">object type PatientDTO</param>
        /// <returns>object type Patient</returns>
        public static Patient PatientDTOToPatient(PatientDTO patientDTO)
        {
            Patient patient = new Patient();

            patient.Jmbg = patientDTO.Jmbg;
            patient.Name = patientDTO.Name;
            patient.Surname = patientDTO.Surname;
            patient.DateOfBirth = patientDTO.DateOfBirth;
            patient.Gender = patientDTO.Gender;
            patient.Phone = patientDTO.Phone;
            patient.CityZipCode = patientDTO.CityZipCode;
            patient.HomeAddress = patientDTO.HomeAddress;
            patient.Email = patientDTO.Email;
            patient.Username = patientDTO.Email;
            patient.Password = patientDTO.Password;
            patient.IsGuest = false;
            patient.IsActive = false;

            return patient;
        }

        /// <summary>
        /// /mapping object type PatientDTO to type PatientCard
        /// </summary>
        /// <param name="patientDTO">object type PatientDTO</param>
        /// <returns>object type PatientCard</returns>
        public static PatientCard PatientDTOToPatientCard(PatientDTO patientDTO)
        {
            PatientCard patientCard = new PatientCard();

            patientCard.BloodType = patientDTO.BloodType;
            patientCard.RhFactor = patientDTO.RhFactor;
            patientCard.Alergies = patientDTO.Alergies;
            patientCard.MedicalHistory = patientDTO.MedicalHistory;
            patientCard.HasInsurance = patientDTO.HasInsurance;
            patientCard.Lbo = patientDTO.Lbo;
            patientCard.PatientJmbg = patientDTO.Jmbg;

            return patientCard;
        }
    }
}
