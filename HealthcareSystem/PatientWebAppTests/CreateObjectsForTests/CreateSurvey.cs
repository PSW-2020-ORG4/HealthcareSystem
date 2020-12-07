using System;
using System.Collections.Generic;
using System.Text;
using Backend;
using Backend.Model;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreateSurvey
    {
        public Survey CreateInvalidTestObject()
        {
            SurveyAboutDoctor surveyAboutDoctor = new SurveyAboutDoctor(
                behaviorOfDoctor: 1, 
                doctorProfessionalism: 3, 
                gettingAdviceByDoctor: 2,
                availabilityOfDoctor: 3,
                examination: new CreateExamination().CreateInvalidTestObjectForSurvey()
            );
            SurveyAboutMedicalStaff surveyAboutMedicalStaff = new SurveyAboutMedicalStaff(
                behaviorOfMedicalStaff: 2,
                medicalStaffProfessionalism: 5, 
                gettingAdviceByMedicalStaff: 3, 
                easeInObtainingFollowUpInformation: 3,
                examination: new CreateExamination().CreateInvalidTestObjectForSurvey()
            );
            SurveyAboutHospital surveyAboutHospital = new SurveyAboutHospital(
                nursing: 5, 
                cleanliness: 1, 
                overallRating: 3, 
                satisfiedWithDrugAndInstrument: 4,
                examination: new CreateExamination().CreateInvalidTestObjectForSurvey()
            );
            return new Survey(surveyAboutDoctor: surveyAboutDoctor, surveyAboutMedicalStaff: surveyAboutMedicalStaff, surveyAboutHospital: surveyAboutHospital);                                    
        }

        public Survey CreateValidTestObject()
        {
            SurveyAboutDoctor surveyAboutDoctor = new SurveyAboutDoctor(
                behaviorOfDoctor: 3, 
                doctorProfessionalism: 3, 
                gettingAdviceByDoctor: 2,                                                                
                availabilityOfDoctor: 3,
                examination: new CreateExamination().CreateValidTestObjectForSurvey()
            );
            SurveyAboutMedicalStaff surveyAboutMedicalStaff = new SurveyAboutMedicalStaff(
                behaviorOfMedicalStaff: 2,
                medicalStaffProfessionalism: 5,
                gettingAdviceByMedicalStaff: 3,
                easeInObtainingFollowUpInformation: 3,
                examination: new CreateExamination().CreateValidTestObjectForSurvey()
            ); 
            SurveyAboutHospital surveyAboutHospital = new SurveyAboutHospital(
                nursing: 5, 
                cleanliness: 4, 
                overallRating: 3, 
                satisfiedWithDrugAndInstrument: 4,
                examination: new CreateExamination().CreateValidTestObjectForSurvey()
            );
            return new Survey(surveyAboutDoctor: surveyAboutDoctor, surveyAboutMedicalStaff: surveyAboutMedicalStaff, surveyAboutHospital: surveyAboutHospital);
        }
    }
}
