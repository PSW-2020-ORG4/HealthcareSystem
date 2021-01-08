using System;
using System.Collections.Generic;
using System.Text;
using UserService.CustomException;
using UserService.Model;
using Xunit;

namespace UserServiceTests.UnitTests
{
    public class AddressTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Validation(string address, bool valid)
        {
            if (valid)
                new Address(address);
            else
                Assert.Throws<ValidationException>(() => new Address(address));
        }

        public static IEnumerable<object[]> Data =>
                                            new List<object[]>
                                            {
                                                new object[] { null, false },
                                                new object[] { "", false },
                                                new object[] { "    \n\t      ", false },
                                                new object[] { "Marka Miljanova 7b", true },
                                                new object[] { "1000 kaplara", true },
                                                new object[] { "Romanijska bb", true }
                                            };
    }
}
