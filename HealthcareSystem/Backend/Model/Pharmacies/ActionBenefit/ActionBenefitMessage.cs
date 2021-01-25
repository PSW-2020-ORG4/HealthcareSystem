using Backend.Model.Pharmacies;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Backend.Model
{
    public class ActionBenefitMessage : ValueObject
    {
        public string Subject { get; private set; }
        public string Message { get; private set; }

        public ActionBenefitMessage() { }

        [JsonConstructor]
        public ActionBenefitMessage(string subject, string message)
        {
            Subject = subject;
            Message = message;
            Validate();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Subject;
            yield return Message;
        }

        private void Validate()
        {
            if (Subject == null || Message == null)
                throw new Exceptions.ValidationException("Subject and/or message is null!");
            Subject = Subject.Trim();
            Message = Message.Trim();
            if (Subject == "" || Message == "")
                throw new Exceptions.ValidationException("Subject and/or message is empty string!");
        }
    }
}
