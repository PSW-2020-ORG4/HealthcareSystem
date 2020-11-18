using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IDeletedPatientCardRepository
    {
        PatientCard GetPatientCard(string jmbg);
        PatientCard AddPatientCard(PatientCard patientCard);
    }
}
