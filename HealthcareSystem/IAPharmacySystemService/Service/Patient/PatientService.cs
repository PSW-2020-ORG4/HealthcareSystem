/***********************************************************************
 * Module:  PatientService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.Users&WorkingTimeService.PatientService
 ***********************************************************************/

using Backend.Model.Users;
using IntegrationAdaptersPharmacySystemService.Repository;
using System.Collections.Generic;

namespace IntegrationAdaptersPharmacySystemService.Service
{
    public class PatientService : IPatientService
    {
        private IActivePatientRepository _activePatientRepository;
        public PatientService(IActivePatientRepository activePatientRepository)
        {
            _activePatientRepository = activePatientRepository;
        }

        public List<Patient> ViewPatients()
        {
            return _activePatientRepository.GetAllPatients();
        }
    }
}