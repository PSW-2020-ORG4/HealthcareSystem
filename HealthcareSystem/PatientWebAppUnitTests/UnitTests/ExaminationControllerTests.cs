using Backend.Service;
using Model.Users;
using Moq;
using PatientWebApp.Controllers;
using Repository;
using System;
using Xunit;
using Model.Enums;
using Microsoft.AspNetCore.Mvc;
using Backend;
using System.Collections.Generic;
using PatientWebApp.DTOs;
using PatientWebAppTests.CreateObjectsForTests;
using Backend.Service.SendingMail;
using System.Threading.Tasks;
using Backend.Model.Users;
using Service.ExaminationAndPatientCard;
using Backend.Model.Exceptions;
using Backend.Service.SearchSpecification.ExaminationSearch;
using Backend.Service.SearchSpecification;


namespace PatientWebAppTests.UnitTests
{
    public class ExaminationControllerTests
    {
        private readonly TestObjectFactory _objectFactory;
        private readonly StubRepository _stubRepository;

        public ExaminationControllerTests()
        {
            _objectFactory = new TestObjectFactory();
            _stubRepository = new StubRepository();
        }


        private ExaminationController SetupExaminationController()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            ExaminationController examinationController = new ExaminationController(examinationService);

            return examinationController;
        }

        [Fact]
        public void Get_existent_examination_by_patient_jmbg()
        {
            ExaminationController examinationController = SetupExaminationController();

            var result = examinationController.GetExaminationsByPatient("1309998775018");

            Assert.True(result is OkObjectResult);
        }


        [Fact]
        public void Get_existent_examination_by_non_patient_jmbg()
        {
            ExaminationController examinationController = SetupExaminationController();

            var result = examinationController.GetExaminationsByPatient("0000000000000");

            Assert.True(result is NotFoundObjectResult);
        }

        [Fact]
        public void Advanced_search_examination()
        {
            ExaminationController examinationController = SetupExaminationController();

            var examinationSearchDTOValidObject = _objectFactory.GetExaminationSearchDTO().CreateValidTestObject();
            var result = examinationController.AdvanceSearchExaminations(examinationSearchDTOValidObject);

            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public void Advanced_search_non_exitent_examination()
        {
            ExaminationController examinationController = SetupExaminationController();

            var examinationSearchDTOInvalidObject = _objectFactory.GetExaminationSearchDTO().CreateInvalidTestObject();
            var result = examinationController.AdvanceSearchExaminations(examinationSearchDTOInvalidObject);

            Assert.True(result is NotFoundObjectResult);
        }


    }
}
