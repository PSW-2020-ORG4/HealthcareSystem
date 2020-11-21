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

namespace PatientWebAppUnitTests
{
    public class PatientControllerTests
    {
        private IActivePatientRepository CreatePatientStubRepository()
        {
            var patientStubRepository = new Mock<IActivePatientRepository>();
            var patients = new List<Patient>();

            patients.Add(new Patient(jmbg: "1234567891234", name: "Pera", surname: "Peric", dateOfBirth: DateTime.Now, gender: GenderType.M,
                            city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                            homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
                            password: "pera", dateOfRegistration: DateTime.Now, isGuest: false));

            patientStubRepository.Setup(m => m.GetPatientByJmbg("1234567891234")).Returns(patients[0]);
            return patientStubRepository.Object;
        }

        private IActivePatientCardRepository CreatePatientCardStubRepository()
        {
            var patientCardStubRepository = new Mock<IActivePatientCardRepository>();
            var patientCards = new List<PatientCard>();

            patientCards.Add(new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.Negativna, alergies: "",
                                   medicalHistory: "", hasInsurance: false, lbo: null, patientJmbg: "1234567891234"));

            patientCardStubRepository.Setup(m => m.GetPatientCardByJmbg("1234567891234")).Returns(patientCards[0]);
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
    }
}
