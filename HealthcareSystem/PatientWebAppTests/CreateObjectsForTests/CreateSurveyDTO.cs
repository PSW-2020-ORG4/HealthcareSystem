using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreateSurveyDTO
    {
        public SurveyDTO CreateInvalidTestObject()
        {
            return new SurveyDTO(id: 1, behaviorOfDoctor: 2, doctorProfessionalism: 3, gettingAdviceByDoctor: 5, availabilityOfDoctor: 4, behaviorOfMedicalStaff: -3,
                                        medicalStaffProfessionalism: 9, gettingAdviceByMedicalStaff: 2, easeInObtainingFollowUpInformation: 5,
                                        nursing: 4, cleanliness: 3, overallRating: 11, satisfiedWithDrugAndInstrument: 2, doctorJmbg: "2211985888888");
        }

        public SurveyDTO CreateValidTestObject()
        {
            return new SurveyDTO( id: 1, behaviorOfDoctor: 2, doctorProfessionalism: 3, gettingAdviceByDoctor: 5, availabilityOfDoctor:4, behaviorOfMedicalStaff: 3, 
                                        medicalStaffProfessionalism: 2, gettingAdviceByMedicalStaff: 2, easeInObtainingFollowUpInformation: 5,
                                        nursing: 4, cleanliness: 3, overallRating: 4, satisfiedWithDrugAndInstrument: 2, doctorJmbg: "2211985888888");
        }
    }
}
