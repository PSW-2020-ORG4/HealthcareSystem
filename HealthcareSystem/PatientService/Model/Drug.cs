using PatientService.CustomException;
using System;

namespace PatientService.Model
{
    public class Drug
    {
        public int Id { get; }
        public string Name { get; }

        public Drug(int id, string name)
        {
            Id = id;
            Name = name;
            Validate();
        }

        private void Validate()
        {
            if (String.IsNullOrWhiteSpace(Name))
                throw new ValidationException("Drug name cannot be empty.");
        }
    }
}
