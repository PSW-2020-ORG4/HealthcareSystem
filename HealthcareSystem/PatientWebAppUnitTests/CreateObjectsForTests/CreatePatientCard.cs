using Model.Enums;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreatePatientCard : ICreateObject
    {
        public object createValidObject()
        {
            return new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.Negativna, alergies: "",
                                   medicalHistory: "", hasInsurance: false, lbo: null, patientJmbg: "1234567891234");
        }
        public object createInvalidObject()
        {
            return new PatientCard(id: 1, bloodType: BloodType.AB, rhFactor: RhFactorType.Negativna, alergies: null,
                                   medicalHistory: null, hasInsurance: false, lbo: null, patientJmbg: null);
        }
        
    }
}
