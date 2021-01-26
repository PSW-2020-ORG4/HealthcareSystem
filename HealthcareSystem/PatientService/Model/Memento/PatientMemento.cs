using System.Collections.Generic;

namespace PatientService.Model.Memento
{
    public class PatientMemento : IMemento
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Jmbg { get; set; }
        public BloodType BloodType { get; set; }
        public RhFactor RhFactor { get; set; }
        public string Allergies { get; set; }
        public string MedicalHistory { get; set; }
        public string InsuranceNumber { get; set; }
        public IEnumerable<ExaminationMemento> Examinations { get; set; }
        public IEnumerable<TherapyMemento> Therapies { get; set; }

        public PatientMemento()
        {
            Examinations = new List<ExaminationMemento>();
            Therapies = new List<TherapyMemento>();
        }
    }
}
