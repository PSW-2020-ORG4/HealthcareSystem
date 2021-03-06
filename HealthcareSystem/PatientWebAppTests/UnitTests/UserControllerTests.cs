﻿using PatientWebAppTests.CreateObjectsForTests;

namespace PatientWebAppTests.UnitTests
{
    public class UserControllerTests
    {
        private readonly TestObjectFactory _objectFactory;
        private readonly StubRepository _stubRepository;
        /*
        public UserControllerTests()
        {
            _objectFactory = new TestObjectFactory();
            _stubRepository = new StubRepository();
        }

        private UserController SetupUserController()
        {
            PatientService patientService = new PatientService(_stubRepository.CreatePatientStubRepository());
            AdminService adminService = new AdminService(_stubRepository.CreateAdminStubRepository());

            UserController userController = new UserController(patientService, adminService);

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

        [Fact]
        public void Login_valid_patient()
        {
            UserController userController = SetupUserController();

            var userCredentialsDTOValidObject = new UserCredentialsDTO { Username = "pera", Password = "12345678" };
            var result = userController.Authenticate(userCredentialsDTOValidObject);

            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public void Login_invalid_user()
        {
            UserController userController = SetupUserController();

            var userCredentialsDTOValidObject = new UserCredentialsDTO { Username = "tamara", Password = "33345678" };
            var result = userController.Authenticate(userCredentialsDTOValidObject);

            Assert.True(result is BadRequestObjectResult);
        }
        */

    }
}
