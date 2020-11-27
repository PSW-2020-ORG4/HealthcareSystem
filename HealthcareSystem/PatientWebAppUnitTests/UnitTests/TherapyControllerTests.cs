using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using PatientWebApp.Controllers;
using PatientWebAppTests.CreateObjectsForTests;
using Service.DrugAndTherapy;
using Xunit;

namespace PatientWebAppTests.UnitTests
{
    public class TherapyControllerTests
    {
        private readonly TestObjectFactory _objectFactory;
        private readonly StubRepository _stubRepository;

        public TherapyControllerTests()
        {
            _objectFactory = new TestObjectFactory();
            _stubRepository = new StubRepository();
        }

        private TherapyController SetupTherapyController()
        {
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            TherapyController therapyController = new TherapyController(therapyService);

            return therapyController;
        }

        [Fact]
        public void Get_existent_therapy_by_patient_jmbg()
        {
            TherapyController therapyController = SetupTherapyController();

            var result = therapyController.GetTherapiesByPatient("1309998775018");

            Assert.True(result is OkObjectResult);
        }


        [Fact]
        public void Get_therapy_by_non_existent_patient_jmbg()
        {
            TherapyController therapyController = SetupTherapyController();

            var result = therapyController.GetTherapiesByPatient("0000000000000");

            Assert.True(result is NotFoundObjectResult);
        }


        [Fact]
        public void Advanced_search_therapy()
        {
            TherapyController therapyController = SetupTherapyController();

            var therapySearchDTOValidObject = _objectFactory.GetTherapySearchDTO().CreateValidTestObject();
            var result = therapyController.AdvanceSearchTherapies(therapySearchDTOValidObject);

            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public void Advanced_search_non_exitent_therapy()
        {
            TherapyController therapyController = SetupTherapyController();

            var therapySearchDTOInvalidObject = _objectFactory.GetTherapySearchDTO().CreateInvalidTestObject();
            var result = therapyController.AdvanceSearchTherapies(therapySearchDTOInvalidObject);

            Assert.True(result is NotFoundObjectResult);
        }
    }
}
