using FeedbackAndSurveyService.CustomException;
using FeedbackAndSurveyService.FeedbackService.Model;
using FeedbackAndSurveyService.FeedbackService.Model.Memento;
using System.Collections.Generic;
using Xunit;

namespace FeedbackAndSurveyTests.UnitTests
{
    public class FeedbackTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Validation(string comment, bool isPublished, bool isAllowedToPublish, bool valid)
        {
            if (valid)
                new Feedback(new FeedbackMemento()
                {
                    Comment = comment,
                    IsPublished = isPublished,
                    IsAllowedToPublish = isAllowedToPublish
                });
            else
                Assert.Throws<ValidationException>(() => new Feedback(new FeedbackMemento()
                {
                    Comment = comment,
                    IsPublished = isPublished,
                    IsAllowedToPublish = isAllowedToPublish
                }));
        }

        public static IEnumerable<object[]> Data =>
                                            new List<object[]>
                                            {
                                                new object[] {  "komentar", true, false, false },
                                                new object[] {  "", false, true, false },
                                                new object[] {  null, false, true, false },
                                                new object[] {  "aa", false, true, true },
                                                new object[] {  "aa", true, true, true },
                                            };
    }
}

