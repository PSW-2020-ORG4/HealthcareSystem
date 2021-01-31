using FeedbackAndSurveyService.CustomException;
using System;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class Grade
    {
        public int Value { get; }

        public Grade(int value)
        {
            Value = value;
            Validate();
        }

        public override bool Equals(object obj)
        {
            return obj is Grade grade &&
                   Value == grade.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        private void Validate()
        {
            if (Value > 5 || Value < 1) throw new ValidationException("Grade is out of range");
        }
    }
}
