using Model.Doctor;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IActivePatientCardRepository
    {
        PatientCard GetPatientCard(string jmbg);
        void AddPatientCard(PatientCard patientCard);
        void DeletePatientCard(string patientJmbg);

        //this method will be moved to the IExaminationRepository when colleagues implement it
        void SaveExaminationInPatientCard(Examination examination);
        void SetPatientCard(PatientCard card);
    }
}
