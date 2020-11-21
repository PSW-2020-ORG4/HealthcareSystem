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
        private readonly ObjectFactory _objectFactory;

        public PatientControllerTests()
        {
            _objectFactory = new ObjectFactory();
        }
        private IActivePatientRepository CreatePatientStubRepository()
        {
            var patientStubRepository = new Mock<IActivePatientRepository>();
            ICreateObject newValidObject = _objectFactory.GetObject("Patient");
            var patients = new List<Patient>();
            patients.Add((Patient)newValidObject.createValidObject());

            patientStubRepository.Setup(m => m.GetPatientByJmbg("1234567891234")).Returns(patients[0]);
            patientStubRepository.Setup(m => m.AddPatient(new Patient()));

            return patientStubRepository.Object;
        }

        private IActivePatientCardRepository CreatePatientCardStubRepository()
        {
            var patientCardStubRepository = new Mock<IActivePatientCardRepository>();
            ICreateObject newValidObject = _objectFactory.GetObject("PatientCard");
            var patientCards = new List<PatientCard>();
            patientCards.Add((PatientCard)newValidObject.createValidObject());

            patientCardStubRepository.Setup(m => m.GetPatientCardByJmbg("1234567891234")).Returns(patientCards[0]);
            patientCardStubRepository.Setup(m => m.AddPatientCard(new PatientCard()));

            return patientCardStubRepository.Object;

        }

        [Fact]
        public void Get_existent_patient_by_jmbg()
        {
            PatientService patientService = new PatientService(CreatePatientStubRepository());
            PatientCardService patientCardService = new PatientCardService(CreatePatientCardStubRepository());
            PatientController patientController = new PatientController(patientService, patientCardService);

            var result = patientController.GetPatientByJmbg("1234567891234") as ObjectResult;

            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public void Get_non_existent_patient_by_jmbg()
        {
            PatientService patientService = new PatientService(CreatePatientStubRepository());
            PatientCardService patientCardService = new PatientCardService(CreatePatientCardStubRepository());
            PatientController patientController = new PatientController(patientService, patientCardService);

            var result = patientController.GetPatientByJmbg("1054789652001") as ObjectResult;

            Assert.True(result is NotFoundObjectResult);
        }

        [Fact]
        public void Add_valid_patient()
        {
            PatientService patientService = new PatientService(CreatePatientStubRepository());
            PatientCardService patientCardService = new PatientCardService(CreatePatientCardStubRepository());
            PatientController patientController = new PatientController(patientService, patientCardService);
            ICreateObject newValidObject = _objectFactory.GetObject("PatientDTO");

            var result = patientController.AddPatient((PatientDTO)newValidObject.createValidObject());

            Assert.True(result is OkResult);
        }

        [Fact]
        public void Add_invalid_patient()
        {
            PatientService patientService = new PatientService(CreatePatientStubRepository());
            PatientCardService patientCardService = new PatientCardService(CreatePatientCardStubRepository());
            PatientController patientController = new PatientController(patientService, patientCardService);
            ICreateObject newInvalidObject = _objectFactory.GetObject("PatientDTO");

            var result = patientController.AddPatient((PatientDTO)newInvalidObject.createInvalidObject());

            Assert.True(result is BadRequestObjectResult);
        }
    }
}
