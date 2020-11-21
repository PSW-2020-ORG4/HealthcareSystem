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
            patientStubRepository.Setup(m => m.AddPatient(new Patient()));

            return patientStubRepository.Object;
        }

        private IActivePatientCardRepository CreatePatientCardStubRepository()
        {
            var patientCardStubRepository = new Mock<IActivePatientCardRepository>();
            var patientCards = new List<PatientCard>();

            patientCards.Add(new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.Negativna, alergies: "",
                                   medicalHistory: "", hasInsurance: false, lbo: null, patientJmbg: "1234567891234"));

            patientCardStubRepository.Setup(m => m.GetPatientCardByJmbg("1234567891234")).Returns(patientCards[0]);
            patientCardStubRepository.Setup(m => m.AddPatientCard(new PatientCard()));

            return patientCardStubRepository.Object;

        }

        private PatientDTO CreateValidPatientDTOForTestingAddMethod()
        {
            PatientDTO patientDTO = new PatientDTO(name:"Ana",surname:"Anic",jmbg:"1206988452102", gender:GenderType.Z,
                                                    dateOfBirth:DateTime.Now,phone:"065432485",countryId:1,countryName:"Srbija",
                                                    cityZipCode:21000,cityName:"Novi Sad",homeAddress:"Bulevar Oslobodjenja 10", 
                                                    bloodType:BloodType.A,rhFactor:RhFactorType.Negativna,hasInsurance:false,lbo:"",
                                                    alergies:"",medicalHistory:"",email:"ana@gmail.com",password:"ana");
            return patientDTO;
        }

        private PatientDTO CreateInvalidPatientDTOForTestingAddMethod()
        {
            PatientDTO patientDTO = new PatientDTO(name: "Ana", surname: "Anic", jmbg: null, gender: GenderType.Z,
                                                    dateOfBirth: DateTime.Now, phone: "", countryId: 1, countryName: "Srbija",
                                                    cityZipCode: 21000, cityName: "Novi Sad", homeAddress: null,
                                                    bloodType: BloodType.A, rhFactor: RhFactorType.Negativna, hasInsurance: false, lbo: "",
                                                    alergies: null, medicalHistory: null, email: null, password: "");
            return patientDTO;
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
            
            var result = patientController.AddPatient(CreateValidPatientDTOForTestingAddMethod());

            Assert.True(result is OkResult);
        }

        [Fact]
        public void Add_invalid_patient()
        {
            PatientService patientService = new PatientService(CreatePatientStubRepository());
            PatientCardService patientCardService = new PatientCardService(CreatePatientCardStubRepository());
            PatientController patientController = new PatientController(patientService, patientCardService);

            var result = patientController.AddPatient(CreateInvalidPatientDTOForTestingAddMethod());

            Assert.True(result is BadRequestObjectResult);
        }
    }
}
