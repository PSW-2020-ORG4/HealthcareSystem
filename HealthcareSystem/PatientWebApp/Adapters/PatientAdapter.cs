using Model.Users;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.Adapters
{
    public class PatientAdapter
    {
        /// <summary>
        /// /addapting object type Patient to type PatientDTO
        /// </summary>
        /// <param name="patient">object type Patient</param>
        /// <returns>object type PatientDTO</returns>
        public static PatientDTO PatientToPatientDTO(Patient patient)
        {
            PatientDTO dto = new PatientDTO();
            dto.Jmbg = patient.Jmbg;
            dto.Name = patient.Name;
            dto.Surname = patient.Surname;

            return dto;
        }
    }
}
