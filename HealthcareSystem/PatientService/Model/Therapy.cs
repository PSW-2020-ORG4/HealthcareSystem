using PatientService.CustomException;
using PatientService.Model.Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Model
{
    public class Therapy : IOriginator<TherapyMemento>
    {
        internal int Id { get; }
        internal Doctor Doctor { get; }
        internal DateRange DateRange { get; }
        internal Drug Drug { get; }
        internal int DailyDose { get; }
        internal string Diagnosis { get; }
        internal int ExaminationId { get; }

        public Therapy(TherapyMemento memento)
        {
            Id = memento.Id;
            Doctor = new Doctor(memento.DoctorJmbg, memento.DoctorName, memento.DoctorSurname);
            DateRange = new DateRange(memento.StartDate, memento.EndDate);
            Drug = new Drug(memento.DrugId, memento.DrugName);
            DailyDose = memento.DailyDose;
            Diagnosis = memento.Diagnosis;
            ExaminationId = memento.ExaminationId;
            Validate();
        }

        public TherapyMemento GetMemento()
        {
            return new TherapyMemento()
            {
                Id = Id,
                DoctorJmbg = Doctor.Jmbg.Value,
                DoctorName = Doctor.Name,
                DoctorSurname = Doctor.Surname,
                DailyDose = DailyDose,
                Diagnosis = Diagnosis,
                DrugId = Drug.Id,
                DrugName = Drug.Name,
                EndDate = DateRange.EndDate,
                StartDate = DateRange.StartDate,
                ExaminationId = ExaminationId
            };
        }

        private void Validate()
        {
            if (DailyDose <= 0)
                throw new ValidationException("Daily dose must be a positive number.");
        }
    }
}
