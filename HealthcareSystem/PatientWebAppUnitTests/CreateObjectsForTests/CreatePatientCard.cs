using Model.Enums;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreatePatientCard : ICreateObject
    {
        public object CreateValidObject()
        {
            return new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "",
                                   medicalHistory: "", hasInsurance: false, lbo: null, patientJmbg: "1234567891234");
        }
        public object CreateInvalidObject()
        {
            return new PatientCard(id: 1, bloodType: BloodType.AB, rhFactor: RhFactorType.NEGATIVE, alergies: null,
                                   medicalHistory: null, hasInsurance: false, lbo: null, patientJmbg: null);
        }
        
    }
}
