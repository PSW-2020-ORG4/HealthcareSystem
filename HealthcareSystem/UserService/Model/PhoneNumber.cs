using System;
using System.Text.RegularExpressions;
using UserService.CustomException;

namespace UserService.Model
{
    public class PhoneNumber
    {
        public string Value { get; }

        public PhoneNumber(string value)
        {
            Value = value;
            Validate();
        }

        public override bool Equals(object obj)
        {
            return obj is PhoneNumber number &&
                   Value == number.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Value)) 
                throw new ValidationException("Phone number cannot be empty.");
            if (!IsValidFormat(Value)) 
                throw new ValidationException("Phone number format is not valid.");
        }

        private bool IsValidFormat(string Value)
        {
            Regex regex = new Regex(@"^[0-9]{6,16}$");
            return regex.IsMatch(Value);
        }
    }
}
