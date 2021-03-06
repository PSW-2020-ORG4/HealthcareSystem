﻿using System;
using UserService.CustomException;

namespace UserService.Model
{
    public class Address
    {
        public string Value { get; }

        public Address(string value)
        {
            Value = value;
            Validate();
        }

        public override bool Equals(object obj)
        {
            return obj is Address address &&
                   Value == address.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        private void Validate()
        {
            if (String.IsNullOrWhiteSpace(Value))
                throw new ValidationException("Address cannot be empty.");
        }
    }
}
