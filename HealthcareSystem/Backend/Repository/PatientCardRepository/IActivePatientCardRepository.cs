﻿
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
        void SetPatientCard(PatientCard card);
    }
}
