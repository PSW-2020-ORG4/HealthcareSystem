using FeedbackAndSurveyService.CustomException;
using FeedbackAndSurveyService.SurveyService.Model;
using System.Collections.Generic;
using Xunit;

namespace FeedbackAndSurveyTests.UnitTests
{
    public class SurvayGradeTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Validation(int value, bool valid)
        {
            if (valid)
                new Grade(value);
            else
                Assert.Throws<ValidationException>(() => new Grade(value));
        }

        public static IEnumerable<object[]> Data =>
                                            new List<object[]>
                                            {
                                                new object[] { null, false },
                                                new object[] { 12345, false },
                                                new object[] { 0, false },
                                                new object[] { -2, false },
                                                new object[] { 1, true },
                                                new object[] { 5, true }

                                            };


    }
}
