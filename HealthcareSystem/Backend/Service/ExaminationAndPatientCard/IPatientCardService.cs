using Backend.Model.Users;

namespace Backend.Service
{
    public interface IPatientCardService
    {
        PatientCard ViewPatientCard(string patientJmbg);
        void AddPatientCard(PatientCard patientCard);
        void EditPatientCard(PatientCard patientCard);

    }
}
