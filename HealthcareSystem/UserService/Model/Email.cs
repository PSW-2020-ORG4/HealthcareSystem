using System;
using System.Text.RegularExpressions;
using UserService.CustomException;

namespace UserService.Model
{
    public class Email
    {
        public string Value { get; }

        public Email(string value)
        {
            Value = value;
            Validate();
        }

        public override bool Equals(object obj)
        {
            return obj is Email email &&
                   Value == email.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Value)) throw new ValidationException("Email can't be empty!");
            if (!IsValidFormat(Value)) throw new ValidationException("Invalid email");
        }

        private bool IsValidFormat(string Value)
        {
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return regex.IsMatch(Value);
        }
    }
}
