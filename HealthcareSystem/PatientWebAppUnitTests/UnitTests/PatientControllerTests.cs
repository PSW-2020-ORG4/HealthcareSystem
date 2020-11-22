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

namespace PatientWebAppTests.UnitTests
{
    public class PatientControllerTests
    {
        private readonly TestObjectFactory _objectFactory;
        private readonly StubRepository _stubRepository;

        public PatientControllerTests()
        {
            _objectFactory = new TestObjectFactory();
            _stubRepository = new StubRepository();
        }
   
        [Fact]
        public void Get_existent_patient_by_jmbg()
        {
            PatientService patientService = new PatientService(_stubRepository.CreatePatientStubRepository());
            PatientCardService patientCardService = new PatientCardService(_stubRepository.CreatePatientCardStubRepository());
            PatientController patientController = new PatientController(patientService, patientCardService);

            var result = patientController.GetPatientByJmbg("1234567891234") as ObjectResult;

            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public void Get_non_existent_patient_by_jmbg()
        {
            PatientService patientService = new PatientService(_stubRepository.CreatePatientStubRepository());
            PatientCardService patientCardService = new PatientCardService(_stubRepository.CreatePatientCardStubRepository());
            PatientController patientController = new PatientController(patientService, patientCardService);

            var result = patientController.GetPatientByJmbg("1054789652001") as ObjectResult;

            Assert.True(result is NotFoundObjectResult);
        }

        [Fact]
        public void Add_valid_patient()
        {
            PatientService patientService = new PatientService(_stubRepository.CreatePatientStubRepository());
            PatientCardService patientCardService = new PatientCardService(_stubRepository.CreatePatientCardStubRepository());
            PatientController patientController = new PatientController(patientService, patientCardService);
            var patientDTOValidObject = _objectFactory.GetPatientDTO().CreateValidTestObject();

            var result = patientController.AddPatient(patientDTOValidObject);

            Assert.True(result is OkResult);
        }

        [Fact]
        public void Add_invalid_patient()
        {
            PatientService patientService = new PatientService(_stubRepository.CreatePatientStubRepository());
            PatientCardService patientCardService = new PatientCardService(_stubRepository.CreatePatientCardStubRepository());
            PatientController patientController = new PatientController(patientService, patientCardService);
            var patientDTOInvalidObject = _objectFactory.GetPatientDTO().CreateInvalidTestObject();

            var result = patientController.AddPatient(patientDTOInvalidObject);

            Assert.True(result is BadRequestObjectResult);
        }
    }
}
