﻿using Backend.Model;
using Backend.Repository;
using Model.Users;
using Moq;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class StubRepository
    {
        private readonly TestObjectFactory _objectFactory;
        public StubRepository()
        {
            _objectFactory = new TestObjectFactory();
        }
        public IActivePatientRepository CreatePatientStubRepository()
        {
            var patientStubRepository = new Mock<IActivePatientRepository>();
            var patientValidObject = _objectFactory.GetPatient().CreateValidTestObject();
            var patients = new List<Patient>();
            patients.Add(patientValidObject);

            patientStubRepository.Setup(m => m.GetPatientByJmbg("1234567891234")).Returns(patients[0]);
            patientStubRepository.Setup(m => m.AddPatient(new Patient()));

            return patientStubRepository.Object;
        }

        public IActivePatientCardRepository CreatePatientCardStubRepository()
        {
            var patientCardStubRepository = new Mock<IActivePatientCardRepository>();
            var patientCardValidObject = _objectFactory.GetPatientCard().CreateValidTestObject();
            var patientCards = new List<PatientCard>();
            patientCards.Add(patientCardValidObject);

            patientCardStubRepository.Setup(m => m.GetPatientCardByJmbg("1234567891234")).Returns(patientCards[0]);
            patientCardStubRepository.Setup(m => m.AddPatientCard(new PatientCard()));

            return patientCardStubRepository.Object;

        }

        public ISurveyRepository CreateSurveyStubRepository()
        {
            var surveyStubRepository = new Mock<ISurveyRepository>();
            var surveyValidObject = _objectFactory.GetSurvey().CreateValidTestObject();
            var surveys = new List<Survey>();
            surveys.Add(surveyValidObject);

            surveyStubRepository.Setup(m => m.AddSurvey(new Survey()));

            return surveyStubRepository.Object;
        }

    }
}
