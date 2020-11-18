using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service
{
    public interface IPatientCardService
    {
        PatientCard ViewPatientCard(string patientJmbg);
        void AddPatientCard(PatientCard patientCard);
        void EditPatientCard(PatientCard patientCard);
        void DeletePatientCard(string patientJmbg);

    }
}
