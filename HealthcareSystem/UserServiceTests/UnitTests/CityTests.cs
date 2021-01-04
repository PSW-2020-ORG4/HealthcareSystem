using System;
using System.Collections.Generic;
using System.Text;
using UserService.CustomException;
using UserService.Model;
using Xunit;

namespace UserServiceTests.UnitTests
{
    public class CityTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Validation(string cityName, bool valid)
        {
            if (valid)
                new City(1, cityName, 0, "Srbija");
            else
                Assert.Throws<ValidationException>(() => new City(1, cityName, 0, "Srbija"));
        }

        public static IEnumerable<object[]> Data =>
                                            new List<object[]>
                                            {
                                                new object[] { null, false },
                                                new object[] { "", false },
                                                new object[] { "    \n\t      ", false },
                                                new object[] { "Beograd", true }
                                            };
    }
}
