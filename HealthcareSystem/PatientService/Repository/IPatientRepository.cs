using PatientService.Model;

namespace PatientService.Repository
{
    public interface IPatientRepository
    {
        Patient Get(string jmbg);

        Patient GetLazy(string jmbg);
        void Update(Patient patient);
        void Add(Patient patient);
    }
}
