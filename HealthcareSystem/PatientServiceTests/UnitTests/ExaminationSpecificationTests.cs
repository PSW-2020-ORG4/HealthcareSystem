using PatientService.Model;
using PatientService.Model.ExaminationSpecification;
using PatientService.Model.Memento;
using PatientService.Model.Specification;
using System;
using System.Collections.Generic;
using Xunit;

namespace PatientServiceTests.UnitTests
{
    public class ExaminationSpecificationTests
    {
        private readonly Examination _examination = new Examination(new ExaminationMemento()
        {
            Id = 1,
            DoctorJmbg = "2711998188734",
            DoctorName = "Doktor",
            DoctorSurname = "Doktorić",
            Anamnesis = "bol u grlu",
            DateAndTime = new DateTime(2020, 12, 21)
        });

        [Theory]
        [MemberData(nameof(EndDate))]
        public void EndDateSpecification(DateTime? endDate, bool expected)
        {
            ISpecification<Examination> specification = new ExaminationEndDateSpecification(endDate);
            bool result = specification.IsSatisfiedBy(_examination);

            Assert.Equal(result, expected);
        }

        public static IEnumerable<object[]> EndDate =>
                                            new List<object[]>
                                            {
                                                new object[] { new DateTime(2020, 12, 21), true },
                                                new object[] { null, true },
                                                new object[] { new DateTime(2020, 12, 15), false },
                                                new object[] { new DateTime(2021, 12, 21), true }
                                            };

        [Theory]
        [MemberData(nameof(StartDate))]
        public void StartDateSpecification(DateTime? startDate, bool expected)
        {
            ISpecification<Examination> specification = new ExaminationStartDateSpecification(startDate);
            bool result = specification.IsSatisfiedBy(_examination);

            Assert.Equal(result, expected);
        }

        public static IEnumerable<object[]> StartDate =>
                                            new List<object[]>
                                            {
                                                new object[] { new DateTime(2020, 12, 21), true },
                                                new object[] { null, true },
                                                new object[] { new DateTime(2020, 12, 15), true },
                                                new object[] { new DateTime(2021, 12, 21), false }
                                            };

        [Theory]
        [InlineData("bol", true)]
        [InlineData("bol u", true)]
        [InlineData("lalallala", false)]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("grl", true)]
        public void AnamnesisSpecification(string anamnesis, bool expected)
        {
            ISpecification<Examination> specification = new ExaminationAnamnesisSpecification(anamnesis);
            bool result = specification.IsSatisfiedBy(_examination);

            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData("Doktor", true)]
        [InlineData("Dok", true)]
        [InlineData("lalallala", false)]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("dok", true)]
        [InlineData("IĆ", true)]
        public void DoctorSpecification(string doctorSurname, bool expected)
        {
            ISpecification<Examination> specification = new ExaminationDoctorSurnameSpecification(doctorSurname);
            bool result = specification.IsSatisfiedBy(_examination);

            Assert.Equal(result, expected);
        }
    }
}
