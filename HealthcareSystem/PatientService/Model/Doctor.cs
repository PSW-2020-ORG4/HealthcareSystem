using PatientService.CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Model
{
    public class Doctor
    {
        public Jmbg Jmbg { get; }
        public string Name { get; }
        public string Surname { get; }

        public Doctor(string jmbg, string name, string surname)
        {
            Jmbg = new Jmbg(jmbg);
            Name = name;
            Surname = surname;
            Validate();
        }

        private void Validate()
        {
            if (String.IsNullOrWhiteSpace(Name))
                throw new ValidationException("Doctor name cannot be empty.");
            if (String.IsNullOrWhiteSpace(Surname))
                throw new ValidationException("Doctor surname cannot be empty.");
        }
    }
}
