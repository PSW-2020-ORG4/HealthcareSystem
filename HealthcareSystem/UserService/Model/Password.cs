﻿using System;

namespace UserService.Model
{
    public class Password
    {
        public string Value { get; }

        public Password(string value)
        {
            Value = value;
            Validate();
        }

        public override bool Equals(object obj)
        {
            return obj is Password password &&
                   Value == password.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        private void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
