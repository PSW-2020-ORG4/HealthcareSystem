using Backend.Model.Users;
using System.Collections.Generic;

namespace IntegrationAdaptersService1.Service
{
    public interface IPatientService
    {
        List<Patient> ViewPatients();
    }
}
