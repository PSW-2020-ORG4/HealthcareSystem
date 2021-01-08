using MimeKit;
using System;
using System.Collections.Generic;

namespace NotificationService
{
    public class ActivationRequest
    {
        public string ActivationLink { get; }
        public string Name { get; }
        public Email Email { get; }

        public ActivationRequest(string name, string email, string activationLink)
        {
            Name = name;
            ActivationLink = activationLink;
            Email = new Email(email);
            Validate();
        }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("username", Name);
            dictionary.Add("email", Email.Value);
            dictionary.Add("activation_link", ActivationLink);
            return dictionary;
        }

        private void Validate()
        {
            if (String.IsNullOrEmpty(ActivationLink))
                throw new ValidationException();
            if (String.IsNullOrEmpty(Name))
                throw new ValidationException();
        }
    }
}
