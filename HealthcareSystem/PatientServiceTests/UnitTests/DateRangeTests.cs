using PatientService.CustomException;
using PatientService.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace PatientServiceTests.UnitTests
{
    public class DateRangeTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Validation(DateTime startDate, DateTime endDate, bool valid)
        {
            if (valid)
                new DateRange(startDate, endDate);
            else
                Assert.Throws<ValidationException>(() => new DateRange(startDate, endDate));
        }

        public static IEnumerable<object[]> Data =>
                                            new List<object[]>
                                            {
                                                new object[] { new DateTime(2021, 1, 1), new DateTime(2020, 12, 21), false },
                                                new object[] { new DateTime(2021, 1, 1), new DateTime(2021, 1, 1), true },
                                                new object[] { new DateTime(2020, 12, 21), new DateTime(2021, 1, 1), true },

                                            };
    }
}
