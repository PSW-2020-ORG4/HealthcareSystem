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
using Backend.Service.Encryption;

namespace PatientWebAppTests.UnitTests
{
    public class PatientControllerTests
    {
        private readonly TestObjectFactory _objectFactory;
        private readonly StubRepository _stubRepository;
        private readonly EncryptionService _encryptionService;

        public PatientControllerTests()
        {
            _objectFactory = new TestObjectFactory();
            _stubRepository = new StubRepository();
            _encryptionService = new EncryptionService();
        }

        private PatientController SetupPatientController(Mock<IMailService> mailMockService)
        {
            PatientService patientService = new PatientService(_stubRepository.CreatePatientStubRepository());
            PatientCardService patientCardService = new PatientCardService(_stubRepository.CreatePatientCardStubRepository());
            IMailService mailService = mailMockService.Object;

            PatientController patientController = new PatientController(patientService, patientCardService, null, mailService);

            return patientController;
        }

        [Fact]
        public void Get_existent_patient_by_jmbg()
        {
            PatientController patientController = SetupPatientController(new Mock<IMailService>());

            var result = patientController.GetPatientByJmbg("1234567891234");

            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public void Get_non_existent_patient_by_jmbg()
        {
            PatientController patientController = SetupPatientController(new Mock<IMailService>());

            var result = patientController.GetPatientByJmbg("1054789652001");

            Assert.True(result is NotFoundObjectResult);
        }

        [Fact]
        public async Task Add_valid_patient_async()
        {
            PatientController patientController = SetupPatientController(new Mock<IMailService>());

            var patientDTOValidObject = _objectFactory.GetPatientDTO().CreateValidTestObject();
            var result = await patientController.AddPatient(patientDTOValidObject);

            Assert.True(result is OkResult);
        }

        [Fact]
        public async Task Add_invalid_patient_async()
        {
            PatientController patientController = SetupPatientController(new Mock<IMailService>());

            var patientDTOInvalidObject = _objectFactory.GetPatientDTO().CreateInvalidTestObject();
            var result = await patientController.AddPatient(patientDTOInvalidObject);

            Assert.True(result is BadRequestObjectResult);
        }

        [Fact]
        public async Task Successfully_sending_mail_for_a_validly_added_patient_async()
        {
            Mock<IMailService> mockService = new Mock<IMailService>();
            PatientController patientController = SetupPatientController(mockService);

            var patientDTOValidObject = _objectFactory.GetPatientDTO().CreateValidTestObject();
            var result = await patientController.AddPatient(patientDTOValidObject);

            mockService.Verify(mock => mock.SendWelcomeEmailAsync(It.IsAny<WelcomeRequest>()), Times.Once());

        }

        [Fact]
        public async Task Unsuccessfully_sending_mail_for_a_invalidly_added_patient_async()
        {
            Mock<IMailService> mockService = new Mock<IMailService>();
            PatientController patientController = SetupPatientController(mockService);

            var patientDTOInvalidObject = _objectFactory.GetPatientDTO().CreateInvalidTestObject();
            var result = await patientController.AddPatient(patientDTOInvalidObject);

            mockService.Verify(mock => mock.SendWelcomeEmailAsync(It.IsAny<WelcomeRequest>()), Times.Never());

        }

        [Fact]
        public void Update_activation_patient()
        {
            PatientController patientController = SetupPatientController(new Mock<IMailService>());

            string encryptedJmbg = _encryptionService.EncryptString("1234567891234");
            var result = patientController.ActivatePatient(encryptedJmbg);

            Assert.True(result is OkResult);
        }

        [Fact]
        public void Update_activation_non_existent_patient()
        {
            PatientController patientController = SetupPatientController(new Mock<IMailService>());

            var result = patientController.ActivatePatient("1054789652001");

            Assert.True(result is NotFoundObjectResult);
        }
    }
}
