using Backend.Model.Users;
using Backend.Service.SendingMail;
﻿using Backend.Model;
using Backend.Repository;
using Model.Users;
using Moq;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Backend.Repository.ExaminationRepository;
using Model.PerformingExamination;
using Backend.Repository.TherapyRepository;
using Model.Enums;
using Model.Manager;

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

            var surveyResultAboutDoctorValidObject = _objectFactory.GetSurveyResultAboutDoctor().CreateValidTestObject();
            var surveyResultsAboutDoctor = new List<SurveyResult> { surveyResultAboutDoctorValidObject };
            var surveyResultAboutHospitalValidObject = _objectFactory.GetSurveyResultAboutHospital().CreateValidTestObject();
            var surveyResultsAboutHospital = new List<SurveyResult> { surveyResultAboutHospitalValidObject };
            var surveyResultAboutMedicalStaffValidObject = _objectFactory.GetSurveyResultAboutMedicalStaff().CreateValidTestObject();
            var surveyResultsAboutMedicalStaff = new List<SurveyResult> { surveyResultAboutMedicalStaffValidObject };

            surveyStubRepository.Setup(m => m.AddSurvey(new Survey()));
            surveyStubRepository.Setup(m => m.GetSurveyResultsAboutDoctor("2211985888888")).Returns(surveyResultsAboutDoctor);
            surveyStubRepository.Setup(m => m.GetSurveyResultsAboutHospital()).Returns(surveyResultsAboutHospital);
            surveyStubRepository.Setup(m => m.GetSurveyResultsAboutMedicalStaff()).Returns(surveyResultsAboutMedicalStaff);

            return surveyStubRepository.Object;
        }

        public IExaminationRepository CreateExaminationStubRepository()
        {
            var examinationStubRepository = new Mock<IExaminationRepository>();
            var examinationValidObject = _objectFactory.GetExamination().CreateValidTestObject();
            var examinations = new List<Examination>();
            examinations.Add(examinationValidObject);          

            examinationStubRepository.Setup(m => m.GetExaminationsByPatient("1309998775018")).Returns(examinations);

            return examinationStubRepository.Object;
        }

        public ITherapyRepository CreateTherapyStubRepository()
        {
            var therapyStubRepository = new Mock<ITherapyRepository>();
            var therapyValidObject = _objectFactory.GetTherapy().CreateValidTestObject();
            var therapies = new List<Therapy>();
            therapies.Add(therapyValidObject);

            therapyStubRepository.Setup(m => m.GetTherapyByPatient("1309998775018")).Returns(therapies);

            return therapyStubRepository.Object;
        }
    }
}
