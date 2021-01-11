using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphicalEditorServer.DTO;
using Backend.Model.Users;

namespace GraphicalEditorServer.Mappers
{
    public class PatientMapper
    {
        public static PatientBasicDTO PatientAndPatientCardToPatientBasicDTO(Patient patient, PatientCard patientCard)
        {
            return new PatientBasicDTO(patient.Name, patient.Surname, patient.Jmbg, patientCard.Id);
        }
    }
}
