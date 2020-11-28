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
            if (CheckIfObjectEmpty(patientDTO))
            {
                throw new ValidationException("Patient cannot be null.");
            }
            if (CheckIfFieldEmpty(patientDTO.Name))
            {
                throw new ValidationException("Name is required field.");
            }
            if (CheckIfFieldEmpty(patientDTO.Surname))
            {
                throw new ValidationException("Surname is required field.");
            }
            if (CheckIfFieldEmpty(patientDTO.Jmbg))
            {
                throw new ValidationException("Unique identification number is required field.");
            }
            if (CheckIfJmbgInvalid(patientDTO.Jmbg))
            {
                throw new ValidationException("Unique identification number must have 13 digits.");
            }
            if (CheckIfFieldEmpty(patientDTO.Phone))
            {
                throw new ValidationException("Phone number is required field.");
            }
            if (CheckIfFieldEmpty(patientDTO.CountryName) || CheckIfFieldEmpty(patientDTO.CityName) || CheckIfFieldEmpty(patientDTO.HomeAddress))
            {
                throw new ValidationException("Country, city and home address are required fields.");
            }
            if (patientDTO.HasInsurance && CheckIfFieldEmpty(patientDTO.Lbo))
            {
                throw new ValidationException("Personal number of the insured is required field if You have insurance.");
            }
            if(CheckIfFieldEmpty(patientDTO.Email))
            {
                throw new ValidationException("Email is required field.");
            }
            if (CheckIfEmailInvalid(patientDTO.Email))
            {
                throw new ValidationException("Your email isn't valid. Please check it and try again.");
            }
            if (CheckIfFieldEmpty(patientDTO.Password))
            {
                throw new ValidationException("Password is required field.");
            }
            if (CheckIfPasswordInvalid(patientDTO.Password))
            {
                throw new ValidationException("Your password isn't valid. It must be between 8 and 20 characters.");
            }
        }

        private bool CheckIfObjectEmpty(PatientDTO patientDTO)
        {
            if (patientDTO == null) return true;
            return false;
        }

        private bool CheckIfFieldEmpty(string field)
        {
            if (field == null || field == "") return true;
            return false;
        }

        private bool CheckIfEmailInvalid(string email)
        {
            Regex emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            Match emailMatch = emailRegex.Match(email);

            if (!emailMatch.Success) return true;
            return false;
        }

        private bool CheckIfPasswordInvalid(string password)
        {
            Regex passwordRegex = new Regex(@"^[a-zA-Z0-9\.\-_]{8,20}$");
            Match passwordMatch = passwordRegex.Match(password);

            if (!passwordMatch.Success) return true;
            return false;
        }

        private bool CheckIfJmbgInvalid(string jmbg)
        {
            Regex jmbgRegex = new Regex(@"^[0-9]{13}$");
            Match jmbgMatch = jmbgRegex.Match(jmbg);

            if (!jmbgMatch.Success) return true;
            return false;
        }
    }
}
