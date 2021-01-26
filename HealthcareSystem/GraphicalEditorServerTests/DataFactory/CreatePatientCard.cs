using Backend.Model.Enums;
using Backend.Model.Users;

namespace GraphicalEditorServerTests.DataFactory
{
    public class CreatePatientCard
    {
        public PatientCard CreateValidTestObject()
        {
            return new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "",
                                   medicalHistory: "", hasInsurance: false, lbo: null, patientJmbg: "1234567891234");
        }
        public PatientCard CreateInvalidTestObject()
        {
            return new PatientCard(id: 1, bloodType: BloodType.AB, rhFactor: RhFactorType.NEGATIVE, alergies: null,
                                   medicalHistory: null, hasInsurance: false, lbo: null, patientJmbg: null);
        }
    }
}
