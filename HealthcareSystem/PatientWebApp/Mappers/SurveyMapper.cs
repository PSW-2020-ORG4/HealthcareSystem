using Backend.Model;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.Mappers
{
    public class SurveyMapper
    {
        public static Survey SurveyDTOToSurvey(SurveyDTO dto)
        {
            SurveyAboutDoctor surveyAboutDoctor = CreateSurveyAboutDoctor(dto);
            SurveyAboutMedicalStaff surveyAboutMedicalStaff = CreateSurveyAboutMedicalStaff(dto);
            SurveyAboutHospital surveyAboutHospital = CreateSurveyAboutHospital(dto);
            return new Survey(surveyAboutDoctor, surveyAboutMedicalStaff, surveyAboutHospital);
        }

        private static SurveyAboutHospital CreateSurveyAboutHospital(SurveyDTO dto)
        {
            SurveyAboutHospital surveyAboutHospital = new SurveyAboutHospital();
            surveyAboutHospital.Nursing = dto.Nursing;
            surveyAboutHospital.Cleanliness = dto.Cleanliness;
            surveyAboutHospital.OverallRating = dto.OverallRating;
            surveyAboutHospital.SatisfiedWithDrugAndInstrument = dto.SatisfiedWithDrugAndInstrument;
            surveyAboutHospital.ExaminationId = dto.ExaminationId;
            return surveyAboutHospital;
        }

        private static SurveyAboutMedicalStaff CreateSurveyAboutMedicalStaff(SurveyDTO dto)
        {
            SurveyAboutMedicalStaff surveyAboutMedicalStaff = new SurveyAboutMedicalStaff();
            surveyAboutMedicalStaff.BehaviorOfMedicalStaff = dto.BehaviorOfMedicalStaff;
            surveyAboutMedicalStaff.MedicalStaffProfessionalism = dto.MedicalStaffProfessionalism;
            surveyAboutMedicalStaff.GettingAdviceByMedicalStaff = dto.GettingAdviceByMedicalStaff;
            surveyAboutMedicalStaff.EaseInObtainingFollowUpInformation = dto.EaseInObtainingFollowUpInformation;
            surveyAboutMedicalStaff.ExaminationId = dto.ExaminationId;
            return surveyAboutMedicalStaff;
        }

        private static SurveyAboutDoctor CreateSurveyAboutDoctor(SurveyDTO dto)
        {
            SurveyAboutDoctor surveyAboutDoctor = new SurveyAboutDoctor();
            surveyAboutDoctor.BehaviorOfDoctor = dto.BehaviorOfDoctor;
            surveyAboutDoctor.DoctorProfessionalism = dto.DoctorProfessionalism;
            surveyAboutDoctor.GettingAdviceByDoctor = dto.GettingAdviceByDoctor;
            surveyAboutDoctor.AvailabilityOfDoctor = dto.AvailabilityOfDoctor;
            surveyAboutDoctor.ExaminationId = dto.ExaminationId;
            return surveyAboutDoctor;
        }
    }
}
