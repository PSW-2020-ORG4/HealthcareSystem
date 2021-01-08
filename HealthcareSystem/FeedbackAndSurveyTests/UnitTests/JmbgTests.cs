using System;
using System.Collections.Generic;
using System.Text;
using FeedbackAndSurveyService.CustomException;
using FeedbackAndSurveyService.FeedbackService.Model;
using Xunit;

namespace FeedbackAndSurveyTests.UnitTests
{
    public class JmbgTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Validation(string jmbg, bool valid)
        {
            if (valid)
                new Jmbg(jmbg);
            else
                Assert.Throws<ValidationException>(() => new Jmbg(jmbg));
        }

        public static IEnumerable<object[]> Data =>
                                            new List<object[]>
                                            {
                                                new object[] { null, false },
                                                new object[] { "", false },
                                                new object[] { "   \n\t   ", false },
                                                new object[] { "this is not a jmbg", false },
                                                new object[] { "12345", false },
                                                new object[] { "2711998188734", true }
                                            };

    }
}
