
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
