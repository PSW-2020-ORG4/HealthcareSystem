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
using Service.ExaminationAndPatientCard;
using Backend.Model.Exceptions;
using Backend.Service.SearchSpecification.ExaminationSearch;
using Backend.Service.SearchSpecification;

namespace PatientWebAppTests.UnitTests
{
    public class ExaminationControllerTests
    {
        private readonly TestObjectFactory _objectFactory;
        private readonly StubRepository _stubRepository;

        public ExaminationControllerTests()
        {
            _objectFactory = new TestObjectFactory();
            _stubRepository = new StubRepository();
        }

        private ExaminationController SetupExaminationController()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            ExaminationController examinationController = new ExaminationController(examinationService);

            return examinationController;
        }

        [Fact]
        public void Get_existent_examination_by_patient_jmbg()
        {
            ExaminationController examinationController = SetupExaminationController();

            var result = examinationController.GetExaminationsByPatient("1309998775018");

            Assert.True(result is OkObjectResult);
        }


        [Fact]
        public void Get_existent_examination_by_non_patient_jmbg()
        {
            ExaminationController examinationController = SetupExaminationController();

            var result = examinationController.GetExaminationsByPatient("0000000000000");

            Assert.True(result is NotFoundObjectResult);
        }


        [Fact]
        public void Advanced_search_examination_operator_and_anamnesis()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            var examinationSearchDTOValidObject = _objectFactory.GetExaminationSearchDTO().CreateValidTestObject();
            var result = examinationService.AdvancedSearch(examinationSearchDTOValidObject);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_examination_operator_or_anamnesis()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());
            
            var result = examinationService.AdvancedSearch(new ExaminationSearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.And, doctorSurname: null, anamnesisOperator: LogicalOperator.Or, anamnesis: "lklklkl"));

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_examination_operator_and_surname()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            var result = examinationService.AdvancedSearch(new ExaminationSearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.And, doctorSurname: "Mar", anamnesisOperator: LogicalOperator.And, anamnesis: null));

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_examination_operator_or_surname()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            var result = examinationService.AdvancedSearch(new ExaminationSearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.Or, doctorSurname: "gdgdg", anamnesisOperator: LogicalOperator.And, anamnesis: null));

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_examination_start_date()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            var result = examinationService.AdvancedSearch(new ExaminationSearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.And, doctorSurname: null, anamnesisOperator: LogicalOperator.And, anamnesis: null));

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_examination_operator_or_end_date()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            var result = examinationService.AdvancedSearch(new ExaminationSearchDTO(jmbg: "1309998775018", startDate: DateTime.Now, endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.And, doctorSurname: null, anamnesisOperator: LogicalOperator.And, anamnesis: null));

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_examination_operator_and_end_date()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            var result = examinationService.AdvancedSearch(new ExaminationSearchDTO(jmbg: "1309998775018", startDate: DateTime.Now, endOperator: LogicalOperator.And, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.And, doctorSurname: null, anamnesisOperator: LogicalOperator.And, anamnesis: null));

            Assert.Empty(result);
        }

        [Fact]
        public void Advanced_search_non_exitent_examination()
        {
            ExaminationService examinationService = new ExaminationService(_stubRepository.CreateExaminationStubRepository());

            var examinationSearchDTOInvalidObject = _objectFactory.GetExaminationSearchDTO().CreateInvalidTestObject();
            var result = examinationService.AdvancedSearch(examinationSearchDTOInvalidObject);

            Assert.Empty(result);
        }

        [Fact]
        public void Is_specification_anamnesis_true()
        {
            ExaminationAnamnesisSpecification anamnesisSpecification = new ExaminationAnamnesisSpecification("bol");
            
            var examinationValidObject = _objectFactory.GetExamination().CreateValidTestObject();
            var result = anamnesisSpecification.IsSatisfiedBy(examinationValidObject);

            Assert.True(result);
        }

        [Fact]
        public void Is_specification_anamnesis_false()
        {
            ExaminationAnamnesisSpecification anamnesisSpecification = new ExaminationAnamnesisSpecification("fdfd");

            var examinationValidObject = _objectFactory.GetExamination().CreateValidTestObject();
            var result = anamnesisSpecification.IsSatisfiedBy(examinationValidObject);

            Assert.False(result);
        }

        [Fact]
        public void Is_specification_doctor_surname_true()
        {
            ExaminationDoctorSurnameSpecification surnameSpecification = new ExaminationDoctorSurnameSpecification("markovic");

            var examinationValidObject = _objectFactory.GetExamination().CreateValidTestObject();
            var result = surnameSpecification.IsSatisfiedBy(examinationValidObject);

            Assert.True(result);
        }

        [Fact]
        public void Is_specification_doctor_surname_false()
        {
            ExaminationDoctorSurnameSpecification surnameSpecification = new ExaminationDoctorSurnameSpecification("kllk");

            var examinationValidObject = _objectFactory.GetExamination().CreateValidTestObject();
            var result = surnameSpecification.IsSatisfiedBy(examinationValidObject);

            Assert.False(result);
        }

        [Fact]
        public void Is_specification_start_date_true()
        {
            ExaminationStartDateSpecification startSpecification = new ExaminationStartDateSpecification(new DateTime(2019, 1, 11));

            var examinationValidObject = _objectFactory.GetExamination().CreateValidTestObject();
            var result = startSpecification.IsSatisfiedBy(examinationValidObject);

            Assert.True(result);
        }

        [Fact]
        public void Is_specification_start_date_false()
        {
            ExaminationStartDateSpecification startSpecification = new ExaminationStartDateSpecification(new DateTime(2020, 12, 12));

            var examinationValidObject = _objectFactory.GetExamination().CreateValidTestObject();
            var result = startSpecification.IsSatisfiedBy(examinationValidObject);

            Assert.False(result);
        }

        [Fact]
        public void Is_specification_end_date_true()
        {
            ExaminationEndDateSpecification endSpecification = new ExaminationEndDateSpecification(new DateTime(2020, 12, 12));

            var examinationValidObject = _objectFactory.GetExamination().CreateValidTestObject();
            var result = endSpecification.IsSatisfiedBy(examinationValidObject);

            Assert.True(result);
        }

        [Fact]
        public void Is_specification_end_date_false()
        {
            ExaminationEndDateSpecification endSpecification = new ExaminationEndDateSpecification(new DateTime(2020, 10, 10));

            var examinationValidObject = _objectFactory.GetExamination().CreateValidTestObject();
            var result = endSpecification.IsSatisfiedBy(examinationValidObject);

            Assert.False(result);
        }
    }
}
