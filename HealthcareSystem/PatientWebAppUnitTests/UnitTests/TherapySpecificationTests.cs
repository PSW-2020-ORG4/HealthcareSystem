using System;
using System.Collections.Generic;
using System.Text;
using Backend.Service.SearchSpecification.TherapySearch;
using PatientWebAppTests.CreateObjectsForTests;
using Xunit;

namespace PatientWebAppTests.UnitTests
{
    public class TherapySpecificationTests
    {
        private readonly TestObjectFactory _objectFactory;
        private readonly StubRepository _stubRepository;

        public TherapySpecificationTests()
        {
            _objectFactory = new TestObjectFactory();
            _stubRepository = new StubRepository();
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
