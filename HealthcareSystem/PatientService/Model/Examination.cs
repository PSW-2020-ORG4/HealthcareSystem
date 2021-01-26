using PatientService.Model.Memento;
using System;

namespace PatientService.Model
{
    public class Examination : IOriginator<ExaminationMemento>
    {
        internal int Id { get; }
        internal DateTime DateAndTime { get; }
        internal string Anamnesis { get; }
        internal Doctor Doctor { get; }

        public Examination(ExaminationMemento memento)
        {
            Id = memento.Id;
            DateAndTime = memento.DateAndTime;
            Anamnesis = memento.Anamnesis;
            Doctor = new Doctor(memento.DoctorJmbg, memento.DoctorName, memento.DoctorSurname);
        }

        public ExaminationMemento GetMemento()
        {
            return new ExaminationMemento()
            {
                Id = Id,
                DateAndTime = DateAndTime,
                Anamnesis = Anamnesis,
                DoctorName = Doctor.Name,
                DoctorSurname = Doctor.Surname,
                DoctorJmbg = Doctor.Jmbg.Value
            };
        }
    }
}
