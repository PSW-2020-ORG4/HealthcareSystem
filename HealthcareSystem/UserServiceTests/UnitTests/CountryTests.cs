using System.Collections.Generic;
using UserService.CustomException;
using UserService.Model;
using Xunit;

namespace UserServiceTests.UnitTests
{
    public class CountryTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Validation(string countryName, bool valid)
        {
            if (valid)
                new Country(1, countryName);
            else
                Assert.Throws<ValidationException>(() => new Country(1, countryName));
        }

        public static IEnumerable<object[]> Data =>
                                            new List<object[]>
                                            {
                                                new object[] { null, false },
                                                new object[] { "", false },
                                                new object[] { "    \n\t      ", false },
                                                new object[] { "Srbija", true }
                                            };
    }
}
