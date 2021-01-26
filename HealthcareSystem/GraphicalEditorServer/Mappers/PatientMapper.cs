using Backend.Model.Users;
using GraphicalEditorServer.DTO;

namespace GraphicalEditorServer.Mappers
{
    public class PatientMapper
    {
        public static PatientBasicDTO PatientAndPatientCardToPatientBasicDTO(Patient patient, PatientCard patientCard)
        {
            return new PatientBasicDTO(patient.Name, patient.Surname, patient.Jmbg, patientCard.Id);
        }

        public static PatientBasicDTO PatientToPatientBasicDTO(Patient patient)
        {
            return new PatientBasicDTO(patient.Name, patient.Surname, patient.Jmbg, patient.PatientCard.Id);
        }
    }
}
