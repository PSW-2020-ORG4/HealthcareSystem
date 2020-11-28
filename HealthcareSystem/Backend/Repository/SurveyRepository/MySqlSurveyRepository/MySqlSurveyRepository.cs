﻿using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class MySqlSurveyRepository : ISurveyRepository
    {
        private readonly MyDbContext _context;

        public MySqlSurveyRepository(MyDbContext context) {  _context = context; }

        public void AddSurvey(Survey survey)
        {
            _context.SurveysAboutDoctor.Add(survey.SurveyAboutDoctor);
            _context.SurveysAboutMedicalStaff.Add(survey.SurveyAboutMedicalStaff);
            _context.SurveysAboutHospital.Add(survey.SurveyAboutHospital);
            _context.SaveChanges();
        }

        public List<SurveyResult> GetSurveyResultsAboutDoctor(string jmbg)
        {
            List<SurveyResult> surveyResults = new List<SurveyResult>();
            SurveyResult behaviorOfDoctor = getSurveyResultAboutBehaviorOfDoctor(jmbg);          
            SurveyResult doctorProfessionalism = getSurveyResultAboutDoctorProfessionalism(jmbg);
            SurveyResult gettingAdviceByDoctor = getSurveyResultAboutGettingAdviceByDoctor(jmbg);
            SurveyResult availabilityOfDoctor = getSurveyResultAboutAvailabilityOfDoctor(jmbg);
            surveyResults.Add(behaviorOfDoctor);
            surveyResults.Add(doctorProfessionalism);
            surveyResults.Add(gettingAdviceByDoctor);
            surveyResults.Add(availabilityOfDoctor);
            return surveyResults;
        }      

        public List<SurveyResult> GetSurveyResultsAboutHospital()
        {
            List<SurveyResult> surveyResults = new List<SurveyResult>();
            SurveyResult nursing = getSurveyResultAboutNursing();
            SurveyResult cleanliness = getSurveyResultAboutCleanliness();
            SurveyResult overallRating = getSurveyResultAboutOverallRating();
            SurveyResult satisfactionWithDrugAndInstrument = getSurveyResultAboutDrugAndInstrument();
            surveyResults.Add(nursing);
            surveyResults.Add(cleanliness);
            surveyResults.Add(overallRating);
            surveyResults.Add(satisfactionWithDrugAndInstrument);
            return surveyResults;
        }   

        public List<SurveyResult> GetSurveyResultsAboutMedicalStaff()
        {
            List<SurveyResult> surveyResults = new List<SurveyResult>();
            SurveyResult behaviorOfMedicalStaff = getSurveyResultAboutBehaviorOfMedicalStaff();
            SurveyResult medicalStaffProfessionalism = getSurveyResultAboutMedicalStaffProfessionalism();
            SurveyResult gettingAdviceByMedicalStaff = getSurveyResultAboutGettingAdviceByMedicalStaff();
            SurveyResult easeInObtainingFollowupInformation = getSurveyResultAboutEaseInObtainingFollowUpInformation();
            surveyResults.Add(behaviorOfMedicalStaff);
            surveyResults.Add(medicalStaffProfessionalism);
            surveyResults.Add(gettingAdviceByMedicalStaff);
            surveyResults.Add(easeInObtainingFollowupInformation);
           
            return surveyResults;
        }

        public List<SurveyAboutDoctor> GetSurveysByDoctor(string jmbg)
        {
            return _context.SurveysAboutDoctor.Where(x => x.DoctorJmbg.Equals(jmbg)).ToList();
        }

        private SurveyResult getSurveyResultAboutBehaviorOfDoctor(string jmbg)
        {
            SurveyResult behaviorOfDoctor = new SurveyResult();
            behaviorOfDoctor.RatedItem = "Behavior of doctor";
            behaviorOfDoctor.AverageRating = GetSurveysByDoctor(jmbg).Average(x => x.BehaviorOfDoctor);
            behaviorOfDoctor.NumberOfGradesOne = GetSurveysByDoctor(jmbg).Count(x => x.BehaviorOfDoctor == 1);
            behaviorOfDoctor.NumberOfGradesTwo = GetSurveysByDoctor(jmbg).Count(x => x.BehaviorOfDoctor == 2);
            behaviorOfDoctor.NumberOfGradesThree = GetSurveysByDoctor(jmbg).Count(x => x.BehaviorOfDoctor == 3);
            behaviorOfDoctor.NumberOfGradesFour = GetSurveysByDoctor(jmbg).Count(x => x.BehaviorOfDoctor == 4);
            behaviorOfDoctor.NumberOfGradesFive = GetSurveysByDoctor(jmbg).Count(x => x.BehaviorOfDoctor == 5);
            return behaviorOfDoctor;
        }

        private SurveyResult getSurveyResultAboutDoctorProfessionalism(string jmbg)
        {
            SurveyResult doctorProfessionalism = new SurveyResult();
            doctorProfessionalism.RatedItem = "Professionalism";
            doctorProfessionalism.AverageRating = GetSurveysByDoctor(jmbg).Average(x => x.DoctorProfessionalism);
            doctorProfessionalism.NumberOfGradesOne = GetSurveysByDoctor(jmbg).Count(x => x.DoctorProfessionalism == 1);
            doctorProfessionalism.NumberOfGradesTwo = GetSurveysByDoctor(jmbg).Count(x => x.DoctorProfessionalism == 2);
            doctorProfessionalism.NumberOfGradesThree = GetSurveysByDoctor(jmbg).Count(x => x.DoctorProfessionalism == 3);
            doctorProfessionalism.NumberOfGradesFour = GetSurveysByDoctor(jmbg).Count(x => x.DoctorProfessionalism == 4);
            doctorProfessionalism.NumberOfGradesFive = GetSurveysByDoctor(jmbg).Count(x => x.DoctorProfessionalism == 5);
            return doctorProfessionalism;
        }

        private SurveyResult getSurveyResultAboutGettingAdviceByDoctor(string jmbg)
        {
            SurveyResult gettingAdviceByDoctor = new SurveyResult();
            gettingAdviceByDoctor.RatedItem = "Getting advice by doctor";
            gettingAdviceByDoctor.AverageRating = GetSurveysByDoctor(jmbg).Average(x => x.GettingAdviceByDoctor);
            gettingAdviceByDoctor.NumberOfGradesOne = GetSurveysByDoctor(jmbg).Count(x => x.GettingAdviceByDoctor == 1);
            gettingAdviceByDoctor.NumberOfGradesTwo = GetSurveysByDoctor(jmbg).Count(x => x.GettingAdviceByDoctor == 2);
            gettingAdviceByDoctor.NumberOfGradesThree = GetSurveysByDoctor(jmbg).Count(x => x.GettingAdviceByDoctor == 3);
            gettingAdviceByDoctor.NumberOfGradesFour = GetSurveysByDoctor(jmbg).Count(x => x.GettingAdviceByDoctor == 4);
            gettingAdviceByDoctor.NumberOfGradesFive = GetSurveysByDoctor(jmbg).Count(x => x.GettingAdviceByDoctor == 5);
            return gettingAdviceByDoctor;
        }

        private SurveyResult getSurveyResultAboutAvailabilityOfDoctor(string jmbg)
        {
            SurveyResult availabilityOfDoctor = new SurveyResult();
            availabilityOfDoctor.RatedItem = "Availability of doctor";
            availabilityOfDoctor.AverageRating = GetSurveysByDoctor(jmbg).Average(x => x.AvailabilityOfDoctor);
            availabilityOfDoctor.NumberOfGradesOne = GetSurveysByDoctor(jmbg).Count(x => x.AvailabilityOfDoctor == 1);
            availabilityOfDoctor.NumberOfGradesTwo = GetSurveysByDoctor(jmbg).Count(x => x.AvailabilityOfDoctor == 2);
            availabilityOfDoctor.NumberOfGradesThree = GetSurveysByDoctor(jmbg).Count(x => x.AvailabilityOfDoctor == 3);
            availabilityOfDoctor.NumberOfGradesFour = GetSurveysByDoctor(jmbg).Count(x => x.AvailabilityOfDoctor == 4);
            availabilityOfDoctor.NumberOfGradesFive = GetSurveysByDoctor(jmbg).Count(x => x.AvailabilityOfDoctor == 5);
            return availabilityOfDoctor;
        }

        private SurveyResult getSurveyResultAboutNursing()
        {
            SurveyResult nursing = new SurveyResult();
            nursing.RatedItem = "Nursing";
            nursing.AverageRating = _context.SurveysAboutHospital.Average(x => x.Nursing);
            nursing.NumberOfGradesOne = _context.SurveysAboutHospital.Count(x => x.Nursing == 1);
            nursing.NumberOfGradesTwo = _context.SurveysAboutHospital.Count(x => x.Nursing == 2);
            nursing.NumberOfGradesThree = _context.SurveysAboutHospital.Count(x => x.Nursing == 3);
            nursing.NumberOfGradesFour = _context.SurveysAboutHospital.Count(x => x.Nursing == 4);
            nursing.NumberOfGradesFive = _context.SurveysAboutHospital.Count(x => x.Nursing == 5);
            return nursing;
        }

        private SurveyResult getSurveyResultAboutCleanliness()
        {
            SurveyResult cleanliness = new SurveyResult();
            cleanliness.RatedItem = "Cleanliness";
            cleanliness.AverageRating = _context.SurveysAboutHospital.Average(x => x.Cleanliness);
            cleanliness.NumberOfGradesOne = _context.SurveysAboutHospital.Count(x => x.Cleanliness == 1);
            cleanliness.NumberOfGradesTwo = _context.SurveysAboutHospital.Count(x => x.Cleanliness == 2);
            cleanliness.NumberOfGradesThree = _context.SurveysAboutHospital.Count(x => x.Cleanliness == 3);
            cleanliness.NumberOfGradesFour = _context.SurveysAboutHospital.Count(x => x.Cleanliness == 4);
            cleanliness.NumberOfGradesFive = _context.SurveysAboutHospital.Count(x => x.Cleanliness == 5);
            return cleanliness;
        }

        private SurveyResult getSurveyResultAboutOverallRating()
        {
            SurveyResult overallRating = new SurveyResult();
            overallRating.RatedItem = "Overall rating";
            overallRating.AverageRating = _context.SurveysAboutHospital.Average(x => x.OverallRating);
            overallRating.NumberOfGradesOne = _context.SurveysAboutHospital.Count(x => x.OverallRating == 1);
            overallRating.NumberOfGradesTwo = _context.SurveysAboutHospital.Count(x => x.OverallRating == 2);
            overallRating.NumberOfGradesThree = _context.SurveysAboutHospital.Count(x => x.OverallRating == 3);
            overallRating.NumberOfGradesFour = _context.SurveysAboutHospital.Count(x => x.OverallRating == 4);
            overallRating.NumberOfGradesFive = _context.SurveysAboutHospital.Count(x => x.OverallRating == 5);
            return overallRating;
        }

        private SurveyResult getSurveyResultAboutDrugAndInstrument()
        {
            SurveyResult satisfactionWithDrugAndInstrument = new SurveyResult();
            satisfactionWithDrugAndInstrument.RatedItem = "Satisfaction with drug and instrument";
            satisfactionWithDrugAndInstrument.AverageRating = _context.SurveysAboutHospital.Average(x => x.SatisfiedWithDrugAndInstrument);
            satisfactionWithDrugAndInstrument.NumberOfGradesOne = _context.SurveysAboutHospital.Count(x => x.SatisfiedWithDrugAndInstrument == 1);
            satisfactionWithDrugAndInstrument.NumberOfGradesTwo = _context.SurveysAboutHospital.Count(x => x.SatisfiedWithDrugAndInstrument == 2);
            satisfactionWithDrugAndInstrument.NumberOfGradesThree = _context.SurveysAboutHospital.Count(x => x.SatisfiedWithDrugAndInstrument == 3);
            satisfactionWithDrugAndInstrument.NumberOfGradesFour = _context.SurveysAboutHospital.Count(x => x.SatisfiedWithDrugAndInstrument == 4);
            satisfactionWithDrugAndInstrument.NumberOfGradesFive = _context.SurveysAboutHospital.Count(x => x.SatisfiedWithDrugAndInstrument == 5);
            return satisfactionWithDrugAndInstrument;
        }

        private SurveyResult getSurveyResultAboutBehaviorOfMedicalStaff()
        {
            SurveyResult behaviorOfMedicalStaff = new SurveyResult();
            behaviorOfMedicalStaff.RatedItem = "Behavior of medical staff";
            behaviorOfMedicalStaff.AverageRating = _context.SurveysAboutMedicalStaff.Average(x => x.BehaviorOfMedicalStaff);
            behaviorOfMedicalStaff.NumberOfGradesOne = _context.SurveysAboutMedicalStaff.Count(x => x.BehaviorOfMedicalStaff == 1);
            behaviorOfMedicalStaff.NumberOfGradesTwo = _context.SurveysAboutMedicalStaff.Count(x => x.BehaviorOfMedicalStaff == 2);
            behaviorOfMedicalStaff.NumberOfGradesThree = _context.SurveysAboutMedicalStaff.Count(x => x.BehaviorOfMedicalStaff == 3);
            behaviorOfMedicalStaff.NumberOfGradesFour = _context.SurveysAboutMedicalStaff.Count(x => x.BehaviorOfMedicalStaff == 4);
            behaviorOfMedicalStaff.NumberOfGradesFive = _context.SurveysAboutMedicalStaff.Count(x => x.BehaviorOfMedicalStaff == 5);
            return behaviorOfMedicalStaff;
        }

        private SurveyResult getSurveyResultAboutMedicalStaffProfessionalism()
        {
            SurveyResult medicalStaffProfessionalism = new SurveyResult();
            medicalStaffProfessionalism.RatedItem = "Professionalism of medical staff";
            medicalStaffProfessionalism.AverageRating = _context.SurveysAboutMedicalStaff.Average(x => x.MedicalStaffProfessionalism);
            medicalStaffProfessionalism.NumberOfGradesOne = _context.SurveysAboutMedicalStaff.Count(x => x.MedicalStaffProfessionalism == 1);
            medicalStaffProfessionalism.NumberOfGradesTwo = _context.SurveysAboutMedicalStaff.Count(x => x.MedicalStaffProfessionalism == 2);
            medicalStaffProfessionalism.NumberOfGradesThree = _context.SurveysAboutMedicalStaff.Count(x => x.MedicalStaffProfessionalism == 3);
            medicalStaffProfessionalism.NumberOfGradesFour = _context.SurveysAboutMedicalStaff.Count(x => x.MedicalStaffProfessionalism == 4);
            medicalStaffProfessionalism.NumberOfGradesFive = _context.SurveysAboutMedicalStaff.Count(x => x.MedicalStaffProfessionalism == 5);
            return medicalStaffProfessionalism;
        }

        private SurveyResult getSurveyResultAboutGettingAdviceByMedicalStaff()
        {
            SurveyResult gettingAdviceByMedicalStaff = new SurveyResult();
            gettingAdviceByMedicalStaff.RatedItem = "Getting advice or help when needed from medical staff";
            gettingAdviceByMedicalStaff.AverageRating = _context.SurveysAboutMedicalStaff.Average(x => x.GettingAdviceByMedicalStaff);
            gettingAdviceByMedicalStaff.NumberOfGradesOne = _context.SurveysAboutMedicalStaff.Count(x => x.GettingAdviceByMedicalStaff == 1);
            gettingAdviceByMedicalStaff.NumberOfGradesTwo = _context.SurveysAboutMedicalStaff.Count(x => x.GettingAdviceByMedicalStaff == 2);
            gettingAdviceByMedicalStaff.NumberOfGradesThree = _context.SurveysAboutMedicalStaff.Count(x => x.GettingAdviceByMedicalStaff == 3);
            gettingAdviceByMedicalStaff.NumberOfGradesFour = _context.SurveysAboutMedicalStaff.Count(x => x.GettingAdviceByMedicalStaff == 4);
            gettingAdviceByMedicalStaff.NumberOfGradesFive = _context.SurveysAboutMedicalStaff.Count(x => x.GettingAdviceByMedicalStaff == 5);
            return gettingAdviceByMedicalStaff;
        }

        private SurveyResult getSurveyResultAboutEaseInObtainingFollowUpInformation()
        {
            SurveyResult easeInObtainingFollowupInformation = new SurveyResult();
            easeInObtainingFollowupInformation.RatedItem = "Ease in obtaining follow up information and care";
            easeInObtainingFollowupInformation.AverageRating = _context.SurveysAboutMedicalStaff.Average(x => x.EaseInObtainingFollowUpInformation);
            easeInObtainingFollowupInformation.NumberOfGradesOne = _context.SurveysAboutMedicalStaff.Count(x => x.EaseInObtainingFollowUpInformation == 1);
            easeInObtainingFollowupInformation.NumberOfGradesTwo = _context.SurveysAboutMedicalStaff.Count(x => x.EaseInObtainingFollowUpInformation == 2);
            easeInObtainingFollowupInformation.NumberOfGradesThree = _context.SurveysAboutMedicalStaff.Count(x => x.EaseInObtainingFollowUpInformation == 3);
            easeInObtainingFollowupInformation.NumberOfGradesFour = _context.SurveysAboutMedicalStaff.Count(x => x.EaseInObtainingFollowUpInformation == 4);
            easeInObtainingFollowupInformation.NumberOfGradesFive = _context.SurveysAboutMedicalStaff.Count(x => x.EaseInObtainingFollowUpInformation == 5);
            return easeInObtainingFollowupInformation;
        }
    }
}
