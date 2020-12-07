using PatientWebAppTests.CreateObjectsForTests;
using System;
using System.Collections.Generic;
using System.Text;
using Model.Users;
using Moq;
using Backend;
using PatientWebApp.Controllers;
using Xunit;
using Service.NotificationSurveyAndFeedback;
using Microsoft.AspNetCore.Mvc;
using Backend.Model;
using Service.ExaminationAndPatientCard;

namespace PatientWebAppTests.UnitTests
{
    public class SurveyControllerTests
    {
        private readonly TestObjectFactory _objectFactory;
        private readonly StubRepository _stubRepository;

        public SurveyControllerTests()
        {
            _objectFactory = new TestObjectFactory();
            _stubRepository = new StubRepository();
        }

        private SurveyController SetupSurveyController()
        {
            SurveyService surveyService = new SurveyService(_stubRepository.CreateSurveyStubRepository());
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            SurveyController surveyController = new SurveyController(surveyService, examinationService);

            return surveyController;
        }

        [Fact]
        public void Add_valid_survey()
        {
            SurveyController surveyController = SetupSurveyController();

            var surveyDTOValidObject = _objectFactory.GetSurveyDTO().CreateValidTestObject();
            var result = surveyController.AddSurvey(surveyDTOValidObject);

            Assert.True(result is OkResult);
        }

        [Fact]
        public void Add_invalid_survey()
        {
            SurveyController surveyController = SetupSurveyController();

            var surveyDTOInvalidObject = _objectFactory.GetSurveyDTO().CreateInvalidTestObject();
            var result = surveyController.AddSurvey(surveyDTOInvalidObject);

            Assert.True(result is BadRequestObjectResult);
        }
       
        [Fact]
        public void Get_survey_results_about_existent_doctor()
        {
            SurveyController surveyController = SetupSurveyController();

            var result = surveyController.GetSurveyResultAboutDoctor("2211985888888");

            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public void Get_survey_results_about_non_existent_doctor()
        {
            SurveyController surveyController = SetupSurveyController();

            var result = surveyController.GetSurveyResultAboutDoctor("2204985888888");

            Assert.True(result is NotFoundObjectResult);
        }

        [Fact]
        public void Get_survey_results_about_hospital()
        {
            SurveyController surveyController = SetupSurveyController();

            var result = surveyController.GetSurveyResultAboutHospital();

            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public void Get_survey_results_about_medical_staff()
        {
            SurveyController surveyController = SetupSurveyController();

            var result = surveyController.GetSurveyResultAboutMedicalStaff();

            Assert.True(result is OkObjectResult);
        }

    }
}
