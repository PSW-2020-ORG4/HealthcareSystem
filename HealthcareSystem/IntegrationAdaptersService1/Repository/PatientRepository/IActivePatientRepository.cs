using Backend.Model.Users;
using System.Collections.Generic;

namespace IntegrationAdaptersService1.Repository
{
    public interface IActivePatientRepository
    {
        List<Patient> GetAllPatients();
    }
}
