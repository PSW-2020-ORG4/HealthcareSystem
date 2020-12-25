using Backend.Service;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Mvc;
using PatientWebApp.Controllers;
using PatientWebApp.DTOs;
using PatientWebAppTests.CreateObjectsForTests;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Xunit;

namespace PatientWebAppTests.UnitTests
{
    public class UserControllerTests
    {
        private readonly TestObjectFactory _objectFactory;
        private readonly StubRepository _stubRepository;

        public UserControllerTests()
        {
            _objectFactory = new TestObjectFactory();
            _stubRepository = new StubRepository();
        }

        private UserController SetupUserController()
        {
            PatientService patientService = new PatientService(_stubRepository.CreatePatientStubRepository());
            AdminService adminService = new AdminService(_stubRepository.CreateAdminStubRepository());

            UserController userController = new UserController(null, patientService, adminService);

            return userController;
        }


        [Fact]
        public void Login_valid_admin()
        {
            UserController userController = SetupUserController();

            var userCredentialsDTOValidObject = new UserCredentialsDTO { Username = "milic_milan@gmail.com", Password = "milanmilic965" };
            var result = userController.Authenticate(userCredentialsDTOValidObject);

            Assert.True(result is OkObjectResult);
        }




    }
}
