using System;
using System.Collections.Generic;
using System.Text;
using Backend;
using Backend.Model;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreateSurvey : ICreateTestObject<Survey>
    {
        public Survey CreateInvalidTestObject()
        {
            return new Survey(behaviorOfDoctor: 0, doctorProfessionalism: 3, gettingAdviceByDoctor: 2, availabilityOfDoctor: 3,
                            behaviorOfMedicalStaff: -2, medicalStaffProfessionalism: 5, gettingAdviceByMedicalStaff: 3, 
                            easeInObtainingFollowupInformationAndCare: 3, nursing: 5, cleanliness: 9, overallRating: 3, 
                            satisfiedWithDrugAndInstrument: 4, doctorJmbg: "");
        }

        public Survey CreateValidTestObject()
        {
            return new Survey(behaviorOfDoctor: 3, doctorProfessionalism: 3, gettingAdviceByDoctor: 2, availabilityOfDoctor: 3,
                            behaviorOfMedicalStaff: 4, medicalStaffProfessionalism: 5, gettingAdviceByMedicalStaff: 3,
                            easeInObtainingFollowupInformationAndCare: 3, nursing: 5, cleanliness: 3, overallRating: 3,
                            satisfiedWithDrugAndInstrument: 4, doctorJmbg: "2211985888888");
        }
    }
}
