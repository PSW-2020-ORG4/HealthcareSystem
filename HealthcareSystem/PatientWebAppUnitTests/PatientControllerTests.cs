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

namespace PatientWebAppUnitTests
{
    public class PatientControllerTests
    {
        private IActivePatientRepository CreatePatientStubRepository()
        {
            var patientStubRepository = new Mock<IActivePatientRepository>();
            var patient = new Patient(jmbg: "1234567891234", name: "Pera", surname: "Peric", dateOfBirth: DateTime.Now, gender: GenderType.M,
                            city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                            homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
                            password: "pera", dateOfRegistration: DateTime.Now, isGuest: false);
            patientStubRepository.Setup(m => m.GetPatientByJmbg("1234567891234")).Returns(patient);
            return patientStubRepository.Object;
        }

        private IActivePatientCardRepository CreatePatientCardStubRepository()
        {
            var patientCardStubRepository = new Mock<IActivePatientCardRepository>();
            var patientCard = new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.Negativna, alergies: "",
                                   medicalHistory: "", hasInsurance: false, lbo: null, patientJmbg: "1234567891234");
            patientCardStubRepository.Setup(m => m.GetPatientCardByJmbg("1234567891234")).Returns(patientCard);
            return patientCardStubRepository.Object;

        }

        [Fact]
        public void Get_patient_by_jmbg()
        {
            PatientService patientService = new PatientService(CreatePatientStubRepository());
            PatientCardService patientCardService = new PatientCardService(CreatePatientCardStubRepository());
            PatientController patientController = new PatientController(patientService, patientCardService);

            var result = patientController.GetPatientByJmbg("1234567891234") as ObjectResult;

            Assert.True(result is OkObjectResult);
        }
    }
}
