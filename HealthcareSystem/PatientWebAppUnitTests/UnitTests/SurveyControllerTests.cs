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


        [Fact]
        public void Add_valid_survey()
        {
            SurveyService surveyService = new SurveyService(_stubRepository.CreateSurveyStubRepository());
            SurveyController surveyController = new SurveyController(surveyService);
            var surveyDTOValidObject = _objectFactory.GetSurveyDTO().CreateValidTestObject();

            var result = surveyController.AddSurvey(surveyDTOValidObject);

            Assert.True(result is OkResult);
        }


        [Fact]
        public void Add_invalid_survey()
        {
            SurveyService surveyService = new SurveyService(_stubRepository.CreateSurveyStubRepository());
            SurveyController surveyController = new SurveyController(surveyService);
            var surveyDTOInvalidObject = _objectFactory.GetSurveyDTO().CreateInvalidTestObject();

            var result = surveyController.AddSurvey(surveyDTOInvalidObject);

            Assert.True(result is BadRequestObjectResult);
        }


    }
}
