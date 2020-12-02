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
            SurveyAboutDoctor surveyAboutDoctor = new SurveyAboutDoctor(
                behaviorOfDoctor: 0, 
                doctorProfessionalism: 3, 
                gettingAdviceByDoctor: 2,
                availabilityOfDoctor: 3,
                examinationId: 1
            );
            SurveyAboutMedicalStaff surveyAboutMedicalStaff = new SurveyAboutMedicalStaff(
                behaviorOfMedicalStaff: -2,
                medicalStaffProfessionalism: 5, 
                gettingAdviceByMedicalStaff: 3, 
                easeInObtainingFollowUpInformation: 3,
                examinationId: 1
            );
            SurveyAboutHospital surveyAboutHospital = new SurveyAboutHospital(
                nursing: 5, 
                cleanliness: 9, 
                overallRating: 3, 
                satisfiedWithDrugAndInstrument: 4,
                examinationId: 1
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
                examinationId: 1
            );
            SurveyAboutMedicalStaff surveyAboutMedicalStaff = new SurveyAboutMedicalStaff(
                behaviorOfMedicalStaff: 2, 
                medicalStaffProfessionalism: 5,
                gettingAdviceByMedicalStaff: 3, 
                easeInObtainingFollowUpInformation: 3,
                examinationId: 1
            );
            SurveyAboutHospital surveyAboutHospital = new SurveyAboutHospital(
                nursing: 5, 
                cleanliness: 4, 
                overallRating: 3, 
                satisfiedWithDrugAndInstrument: 4,
                examinationId: 1
            );
            return new Survey(surveyAboutDoctor: surveyAboutDoctor, surveyAboutMedicalStaff: surveyAboutMedicalStaff, surveyAboutHospital: surveyAboutHospital);
        }
    }
}
