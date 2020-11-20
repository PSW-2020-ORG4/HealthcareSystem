using Backend.Model.Exceptions;
using Backend.Service;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PatientWebApp.Validators
{
    public class PatientValidator
    {
        public PatientValidator() { }

        public void validatePatientFields(PatientDTO patientDTO)
        {
            if (patientDTO == null)
            {
                throw new ValidationException("Patient cannot be null.");
            }
            if (patientDTO.Name == null || patientDTO.Name == "")
            {
                throw new ValidationException("Name is required field.");
            }
            if (patientDTO.Surname == null || patientDTO.Surname == "")
            {
                throw new ValidationException("Surname is required field.");
            }
            if (patientDTO.Jmbg == null || patientDTO.Jmbg == "")
            {
                throw new ValidationException("Unique identification number is required field.");
            }
            if (patientDTO.Phone == null || patientDTO.Phone == "")
            {
                throw new ValidationException("Phone number is required field.");
            }
            if (patientDTO.CountryName == null || patientDTO.CountryName == "" ||
                patientDTO.CityName == null || patientDTO.CityName == "" ||
                patientDTO.HomeAddress == null || patientDTO.HomeAddress == "")
            {
                throw new ValidationException("Country, city and home address are required fields.");
            }
            if (patientDTO.HasInsurance && (patientDTO.Lbo == null || patientDTO.Lbo == ""))
            {
                throw new ValidationException("Personal number of the insured is required field if You have insurance.");
            }
            if(patientDTO.Email == null || patientDTO.Email == "")
            {
                throw new ValidationException("Email is required field.");
            }
            if (patientDTO.Password == null || patientDTO.Password == "")
            {
                throw new ValidationException("Password is required field.");
            }
        }

        public void checkIfEmailValid(string email)
        {
            Regex emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            Match emailMatch = emailRegex.Match(email);

            if (!emailMatch.Success)
            {
                throw new ValidationException("Your email isn't valid. Please check it and try again.");
            }
        }

        public void checkIfPasswordValid(string password)
        {
            Regex passwordRegex = new Regex(@"^[a-zA-Z0-9\.\-_]{8,20}$");
            Match passwordMatch = passwordRegex.Match(password);

            if (!passwordMatch.Success)
            {
                throw new ValidationException("Your password isn't valid. It must be between 8 and 20 characters.");
            }
        }
    }
}
