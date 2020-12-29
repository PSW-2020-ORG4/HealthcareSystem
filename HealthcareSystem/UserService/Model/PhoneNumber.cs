using System;
using System.Text.RegularExpressions;
using UserService.CustomException;

namespace UserService.Model
{
    public class PhoneNumber
    {
        private string Value { get; }

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
            if (string.IsNullOrEmpty(Value)) throw new ValidationException("Phone number does not exist!");
            if (!IsGoodFormat(Value)) throw new ValidationException("Invalid phone number");
        }

        private bool IsGoodFormat(string Value)
        {
            Regex regex = new Regex(@"^[0-9]{6,16}$");
            return regex.IsMatch(Value);
        }
    }
}
