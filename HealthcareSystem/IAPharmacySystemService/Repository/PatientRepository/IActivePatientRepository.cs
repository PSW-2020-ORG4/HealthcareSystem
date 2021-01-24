using Backend.Model.Users;
using System.Collections.Generic;

namespace IntegrationAdaptersPharmacySystemService.Repository
{
    public interface IActivePatientRepository
    {
        List<Patient> GetAllPatients();
    }
}
