
using Backend.Model.Users;

namespace Backend.Repository
{
    public interface IActivePatientCardRepository
    {
        PatientCard GetPatientCardByJmbg(string jmbg);
        void AddPatientCard(PatientCard patientCard);
        void DeletePatientCard(string patientJmbg);
        void UpdatePatientCard(PatientCard card);
        bool CheckIfPatientCardExists(int patientCardId);
    }
}
