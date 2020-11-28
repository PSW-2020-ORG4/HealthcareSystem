using System;
using System.Collections.Generic;
using System.Text;
using Backend.Service.SearchSpecification;
using Backend.Service.SearchSpecification.TherapySearch;
using Microsoft.AspNetCore.Mvc;
using PatientWebApp.Controllers;
using PatientWebAppTests.CreateObjectsForTests;
using Service.DrugAndTherapy;
using Xunit;

namespace PatientWebAppTests.UnitTests
{
    public class TherapyControllerTests
    {
        private readonly TestObjectFactory _objectFactory;
        private readonly StubRepository _stubRepository;

        public TherapyControllerTests()
        {
            _objectFactory = new TestObjectFactory();
            _stubRepository = new StubRepository();
        }

        private TherapyController SetupTherapyController()
        {
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            TherapyController therapyController = new TherapyController(therapyService);

            return therapyController;
        }

        [Fact]
        public void Get_existent_therapy_by_patient_jmbg()
        {
            TherapyController therapyController = SetupTherapyController();

            var result = therapyController.GetTherapiesByPatient("1309998775018");

            Assert.True(result is OkObjectResult);
        }


        [Fact]
        public void Get_therapy_by_non_existent_patient_jmbg()
        {
            TherapyController therapyController = SetupTherapyController();

            var result = therapyController.GetTherapiesByPatient("0000000000000");

            Assert.True(result is NotFoundObjectResult);
        }
        
        [Fact]
        public void Advanced_search_therapy_operator_and_drug_name()
        {
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            var therapySearchDTOValidObject = _objectFactory.GetTherapySearchDTO().CreateValidTestObject();
            var result = therapyService.AdvancedSearch(therapySearchDTOValidObject);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Advanced_search_therapy_operator_or_drug_name()
        {
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            var result = therapyService.AdvancedSearch(new TherapySearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.And, doctorSurname: null, drugOperator: LogicalOperator.Or, drugName: "lklklkl"));

            Assert.NotEmpty(result);
        }
        
        [Fact]
        public void Advanced_search_therapy_and_surname()
        {
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            var result = therapyService.AdvancedSearch(new TherapySearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.And, doctorSurname: "Markovic", drugOperator: LogicalOperator.Or, drugName: "lklklkl"));

            Assert.NotEmpty(result);
        }
        
        [Fact]
        public void Advanced_search_therapy_or_surname()
        {
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            var result = therapyService.AdvancedSearch(new TherapySearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.Or, doctorSurname: "jjjk", drugOperator: LogicalOperator.Or, drugName: "lklklkl"));

            Assert.NotEmpty(result);
        }
        
        [Fact]
        public void Advanced_search_therapy_start_date()
        {
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            var result = therapyService.AdvancedSearch(new TherapySearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 12, 12), doctorOperator: LogicalOperator.Or, doctorSurname: "jjjk", drugOperator: LogicalOperator.Or, drugName: "lklklkl"));

            Assert.NotEmpty(result);
        }
        
        [Fact]
        public void Advanced_search_therapy_or_end_date()
        {
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            var result = therapyService.AdvancedSearch(new TherapySearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.Or, endDate: new DateTime(2020, 10, 10), doctorOperator: LogicalOperator.Or, doctorSurname: "jjjk", drugOperator: LogicalOperator.Or, drugName: "lklklkl"));

            Assert.NotEmpty(result);
        }
        
        [Fact]
        public void Advanced_search_therapy_and_end_date()
        {
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            var result = therapyService.AdvancedSearch(new TherapySearchDTO(jmbg: "1309998775018", startDate: new DateTime(2020, 10, 10), endOperator: LogicalOperator.And, endDate: new DateTime(2020, 10, 10), doctorOperator: LogicalOperator.Or, doctorSurname: "jjjk", drugOperator: LogicalOperator.Or, drugName: "lklklkl"));

            Assert.Empty(result);
        }
        
        [Fact]
        public void Advanced_search_non_exitent_examination()
        {        
            TherapyService therapyService = new TherapyService(_stubRepository.CreateTherapyStubRepository());

            var therapySearchDTOInvalidObject = _objectFactory.GetTherapySearchDTO().CreateInvalidTestObject();
            var result = therapyService.AdvancedSearch(therapySearchDTOInvalidObject);

            Assert.Empty(result);
        }
        
        [Fact]
        public void Is_specification_drug_name_true()
        {
            TherapyDrugNameSpacification drugNameSpecification = new TherapyDrugNameSpacification("par");

            var therapyValidObject = _objectFactory.GetTherapy().CreateValidTestObject();
            var result = drugNameSpecification.IsSatisfiedBy(therapyValidObject);

            Assert.True(result);
        }
        
        [Fact]
        public void Is_specification_drug_name_false()
        {
            TherapyDrugNameSpacification drugNameSpecification = new TherapyDrugNameSpacification("fdfd");

            var therapyValidObject = _objectFactory.GetTherapy().CreateValidTestObject();
            var result = drugNameSpecification.IsSatisfiedBy(therapyValidObject);

            Assert.False(result);
        }
        
        [Fact]
        public void Is_specification_doctor_surname_true()
        {
            TherapyDoctorSurnameSpecification doctorSurnameSpecification = new TherapyDoctorSurnameSpecification("Mark");

            var therapyValidObject = _objectFactory.GetTherapy().CreateValidTestObject();
            var result = doctorSurnameSpecification.IsSatisfiedBy(therapyValidObject);

            Assert.True(result);
        }
        
        [Fact]
        public void Is_specification_doctor_surname_false()
        {
            TherapyDoctorSurnameSpecification doctorSurnameSpecification = new TherapyDoctorSurnameSpecification("fdfd");

            var therapyValidObject = _objectFactory.GetTherapy().CreateValidTestObject();
            var result = doctorSurnameSpecification.IsSatisfiedBy(therapyValidObject);

            Assert.False(result);
        }
        
        [Fact]
        public void Is_specification_start_date_true()
        {
            TherapyStartDateSpecification dateSpecification = new TherapyStartDateSpecification(new DateTime(2020, 10, 10));

            var therapyValidObject = _objectFactory.GetTherapy().CreateValidTestObject();
            var result = dateSpecification.IsSatisfiedBy(therapyValidObject);

            Assert.True(result);
        }

        [Fact]
        public void Is_specification_start_date_false()
        {
            TherapyStartDateSpecification dateSpecification = new TherapyStartDateSpecification(new DateTime(2020, 12, 12));

            var therapyValidObject = _objectFactory.GetTherapy().CreateValidTestObject();
            var result = dateSpecification.IsSatisfiedBy(therapyValidObject);

            Assert.False(result);
        }

        [Fact]
        public void Is_specification_end_date_true()
        {
            TherapyEndDateSpecification dateSpecification = new TherapyEndDateSpecification(new DateTime(2020, 12, 12));

            var therapyValidObject = _objectFactory.GetTherapy().CreateValidTestObject();
            var result = dateSpecification.IsSatisfiedBy(therapyValidObject);

            Assert.True(result);
        }

        [Fact]
        public void Is_specification_end_date_false()
        {
            TherapyEndDateSpecification dateSpecification = new TherapyEndDateSpecification(new DateTime(2020, 10, 10));

            var therapyValidObject = _objectFactory.GetTherapy().CreateValidTestObject();
            var result = dateSpecification.IsSatisfiedBy(therapyValidObject);

            Assert.False(result);
        }
    }
}
