using Backend.Model.Users;
using Backend.Model;
using Model.Users;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Model.PerformingExamination;
using Backend.Service.SearchSpecification.ExaminationSearch;


namespace PatientWebAppTests.CreateObjectsForTests
{
    public class TestObjectFactory
    {
        public CreatePatientDTO GetPatientDTO()
        {
            return new CreatePatientDTO();
        }

        public CreatePatient GetPatient()
        {
            return new CreatePatient();
        }

        public CreatePatientCard GetPatientCard()
        {
            return new CreatePatientCard();
        }

        public CreateSurveyDTO GetSurveyDTO()
        {
            return new CreateSurveyDTO();
        }

        public CreateSurvey GetSurvey()
        {
            return new CreateSurvey();
        }

        public CreateExamination GetExamination()
        {
            return new CreateExamination();
        }

        public CreateTherapy GetTherapy()
        {
            return new CreateTherapy();
        }

        public CreateSurveyResult GetSurveyResultAboutDoctor()
        {
            return new CreateSurveyResult();
        }

        public CreateSurveyResult GetSurveyResultAboutHospital()
        {
            return new CreateSurveyResult();
        }

        public CreateSurveyResult GetSurveyResultAboutMedicalStaff()
        {
            return new CreateSurveyResult();
        }

        public CreateDoctor GetDoctor()
        {
            return new CreateDoctor();
        }

        public CreateRoom GetRoom()
        {
            return new CreateRoom();
        }

        public CreateAdmin GetAdmin()
        {
            return new CreateAdmin();
        }
    }
}
