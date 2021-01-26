using System;
using System.Collections.Generic;
using System.Text;
using Backend.Service.SearchSpecification;
using Microsoft.AspNetCore.Mvc;
using Model.PerformingExamination;
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
        /*
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

        private TherapyService SetupTherapyService()
        {
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            return therapyService;
        }

        [Fact]
        public void Get_existent_therapy_by_patient_jmbg()
        {
            TherapyService therapyService = SetupTherapyService();

            //var result = therapyController.GetTherapiesByPatient("1309998775018");
            var result = therapyService.GetTherapyByPatient("1309998775018");

            Assert.True(result is List<Therapy>);
        }

        [Fact]
        public void Get_therapy_by_non_existent_patient_jmbg()
        {
            TherapyService therapyService = SetupTherapyService();

            var result = therapyService.GetTherapyByPatient("0000000000000");

            Assert.True(result is NotFoundObjectResult);
        }

        [Fact]
        public void Advanced_search_therapy()
        {
            TherapyService therapyService = SetupTherapyService();

            var therapySearchDTOValidObject = _objectFactory.GetTherapySearchDTO().CreateValidTestObject();
            var result = therapyService.AdvancedSearch(therapySearchDTOValidObject);

            Assert.True(result is List<Therapy>);
        }

        [Fact]
        public void Advanced_search_non_exitent_therapy()
        {
            TherapyService therapyService = SetupTherapyService();

            var therapySearchDTOInvalidObject = _objectFactory.GetTherapySearchDTO().CreateInvalidTestObject();
            var result = therapyService.AdvancedSearch(therapySearchDTOInvalidObject);

            Assert.True(result is null);
        }*/
    }
}
