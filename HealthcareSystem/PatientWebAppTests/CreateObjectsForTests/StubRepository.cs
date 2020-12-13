using Backend.Model;
using Backend.Repository;
using Model.Users;
using Moq;
using Repository;
using System.Collections.Generic;
using Backend.Repository.ExaminationRepository;
using Model.PerformingExamination;
using Backend.Repository.TherapyRepository;
using Backend.Model.Enums;
using Backend.Repository.RoomRepository;
using Model.Manager;
using Model.Enums;
using Backend;
using System;

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
            patientCardStubRepository.Setup(m => m.CheckIfPatientCardExists(1)).Returns(true);

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

            Examination examinationCanBeCanceled = _objectFactory.GetExamination().CreateValidCanBeCanceledTestObject();
            Examination examinationCantBeCanceled = _objectFactory.GetExamination().CreateValidCantBeCanceledTestObject();
            Examination examinationValidForSurvey = _objectFactory.GetExamination().CreateValidTestObjectForSurvey();
            Examination examinationInvalidForSurvey = _objectFactory.GetExamination().CreateInvalidTestObjectForSurvey();
            List<Examination> examinations = _objectFactory.GetExamination().CreateValidTestObjects();
            List<Examination> canceledExaminations = GetCanceledExamination(examinations);
            List<Examination> previousExaminations = GetPreviousExaminations(examinations);

            List<Examination> searchExaminations = _objectFactory.GetExamination().CreateValidSearchTestObjects();
            examinationStubRepository.Setup(m => m.GetExaminationsByRoomAndDateTime(1, It.IsAny<DateTime>())).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByDoctorAndDateTime("0909965768767", It.IsAny<DateTime>())).Returns(new List<Examination>());
            examinationStubRepository.Setup(m => m.GetExaminationsByPatientAndDateTime(1, It.IsAny<DateTime>())).Returns(new List<Examination>());

            examinationStubRepository.Setup(m => m.AddExamination(new Examination()));

            examinationStubRepository.Setup(m => m.GetExaminationById(1)).Returns(examinationCanBeCanceled);
            examinationStubRepository.Setup(m => m.GetExaminationById(2)).Returns(examinationCantBeCanceled);
            examinationStubRepository.Setup(m => m.GetExaminationById(9)).Returns(examinationValidForSurvey);
            examinationStubRepository.Setup(m => m.GetExaminationById(10)).Returns(examinationInvalidForSurvey);
            examinationStubRepository.Setup(m => m.GetExaminationsByPatient("1309998775018")).Returns(examinations);
            examinationStubRepository.Setup(m => m.GetPreviousExaminationsByPatient("1309998775018")).Returns(previousExaminations);
            examinationStubRepository.Setup(m => m.GetFollowingExaminationsByPatient("1309998775018")).Returns(examinations);
            examinationStubRepository.Setup(m => m.GetCanceledExaminationsByPatient("1309998775018")).Returns(canceledExaminations);

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
        private List<Examination> GetCanceledExamination(List<Examination> examinations)
        {
            examinations.ForEach(e => e.ExaminationStatus = ExaminationStatus.CANCELED);
            return examinations;
        }

        private List<Examination> GetPreviousExaminations(List<Examination> examinations)
        {
            examinations.ForEach(e => e.ExaminationStatus = ExaminationStatus.FINISHED);
            return examinations;
        }
        public IRoomRepository CreateRoomStubRepository()
        {
            var roomStubRepository = new Mock<IRoomRepository>();
            var roomValidObject = _objectFactory.GetRoom().CreateValidTestObject();
            var rooms = new List<Room>();
            rooms.Add(roomValidObject);

            roomStubRepository.Setup(m => m.GetRoomByNumber(1)).Returns(rooms[0]);
            //roomStubRepository.Setup(m => m.GetRoomsByUsageAndEquipment(TypeOfUsage.CONSULTING_ROOM, new List<int>())).Returns(rooms);
            roomStubRepository.Setup(m => m.CheckIfRoomExists(1)).Returns(true);

            return roomStubRepository.Object;
        }
        public IDoctorRepository CreateDoctorStubRepository()
        {
            var doctorStubRepository = new Mock<IDoctorRepository>();
            var doctorValidObject = _objectFactory.GetDoctor().CreateValidTestObject();
            var doctors = new List<Doctor>();
            doctors.Add(doctorValidObject);

            doctorStubRepository.Setup(m => m.GetDoctorByJmbg("0909965768767")).Returns(doctors[0]);
            doctorStubRepository.Setup(m => m.CheckIfDoctorExists("0909965768767")).Returns(true);

            return doctorStubRepository.Object;
        }
    }
}
