using Backend.Model.Users;
using System.Collections.Generic;

namespace IntegrationAdaptersPharmacySystemService.Service
{
    public interface IPatientService
    {
        List<Patient> ViewPatients();
    }
}
