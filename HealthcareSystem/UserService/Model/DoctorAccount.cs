using System;
using System.Collections.Generic;
using System.Linq;
using UserService.Model.Memento;
using UserService.CustomException;

namespace UserService.Model
{
    public class DoctorAccount : UserAccount
    {
        private DateTime DateOfEmployment { get; }
        private IEnumerable<Specialty> Specialties { get; }

        public DoctorAccount(DoctorAccountMemento memento) : base(memento)
        {
            Specialties = memento.Specialties.Select(s => new Specialty(s));
            Validate();
        }

        public DoctorAccountMemento GetDoctorMemento()
        {
            return new DoctorAccountMemento()
            {
                Jmbg = Jmbg.Value,
                Name = Name,
                Surname = Surname,
                Gender = Gender,
                DateOfBirth = DateOfBirth,
                Phone = Phone.Value,
                HomeAddress = HomeAddress.Value,
                City = City.GetMemento(),
                Email = Email.Value,
                Password = Password.Value,
                UserType = UserType,
                Specialties = Specialties.Select(s => s.GetMemento())
            };
        }

        public override UserAccountMemento GetMemento()
        {
            return GetDoctorMemento();
        }

        protected override void Validate()
        {
            base.Validate();
            if (DateOfEmployment.CompareTo(DateTime.Now) > 0)
            {
                throw new ValidationException("Date of employment not valid.");
            }
        }
    }
}
