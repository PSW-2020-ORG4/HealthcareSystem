using System;
using System.Text.RegularExpressions;

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
            if (IsRegular(Value))
            {
                throw new Exception();
            }
        }

        private bool IsRegular(string Value)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\.\-_]{5,13}[a-zA-Z0-9\.\-_]{5,13}$");
            return regex.Match(Value).Success;
        }
    }
}
