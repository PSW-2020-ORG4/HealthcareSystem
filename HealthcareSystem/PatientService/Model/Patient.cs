using PatientService.CustomException;
using PatientService.Model.Memento;
using PatientService.Model.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Model
{
    public class Patient : IOriginator<PatientMemento>
    {
        private string Name { get; }
        private string Surname { get; }
        private Jmbg Jmbg { get; }
        private BloodType BloodType { get; }
        private RhFactor RhFactor { get; }
        private string Allergies { get; }
        private string MedicalHistory { get; }
        private InsuranceNumber InsuranceNumber { get; }
        public IEnumerable<Examination> Examinations { get; }
        public IEnumerable<Therapy> Therapies { get; }

        public Patient(PatientMemento memento)
        {
            Name = memento.Name;
            Surname = memento.Surname;
            Jmbg = new Jmbg(memento.Jmbg);
            BloodType = memento.BloodType;
            RhFactor = memento.RhFactor;
            Allergies = memento.Allergies;
            MedicalHistory = memento.MedicalHistory;
            InsuranceNumber = new InsuranceNumber(memento.InsuranceNumber);
            Examinations = memento.Examinations.Select(e => new Examination(e));
            Therapies = memento.Therapies.Select(t => new Therapy(t));
            Validate();
        }

        public IEnumerable<Examination> SearchExaminations(ISpecification<Examination> specification)
        {
            return Examinations.Where(e => specification.IsSatisfiedBy(e));
        }

        public IEnumerable<Therapy> SearchTherapies(ISpecification<Therapy> specification)
        {
            return Therapies.Where(t => specification.IsSatisfiedBy(t));
        }

        public PatientMemento GetMemento()
        {
            return new PatientMemento()
            {
                Name = Name,
                Surname = Surname,
                Jmbg = Jmbg.Value,
                Allergies = Allergies,
                MedicalHistory = MedicalHistory,
                BloodType = BloodType,
                RhFactor = RhFactor,
                InsuranceNumber = InsuranceNumber.Value,
                Examinations = Examinations.Select(e => e.GetMemento()),
                Therapies = Therapies.Select(t => t.GetMemento())
            };
        }

        private void Validate()
        {
            if (String.IsNullOrWhiteSpace(Name))
                throw new ValidationException("Name cannot be empty.");
            if (String.IsNullOrWhiteSpace(Surname))
                throw new ValidationException("Surname cannot be empty.");
        }
    }
}
