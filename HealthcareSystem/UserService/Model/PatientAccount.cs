using System;
using System.Collections.Generic;
using System.Linq;
using UserService.Model.Memento;
using UserService.CustomException;

namespace UserService.Model
{
    public class PatientAccount : UserAccount
    {
        private bool IsActivated { get; set; }
        private bool IsBlocked { get; set; }
        private string ImageName { get; }
        private IEnumerable<MaliciousAction> MaliciousActions { get; }

        public PatientAccount(PatientAccountMemento memento)
        {
            Jmbg = new Jmbg(memento.Jmbg);
            Name = memento.Name;
            Surname = memento.Surname;
            Gender = memento.Gender;
            DateOfBirth = memento.DateOfBirth;
            Phone = new PhoneNumber(memento.Phone);
            HomeAddress = new Address(memento.HomeAddress);
            City = new City(memento.City);
            Email = new Email(memento.Email);
            Password = new Password(memento.Password);
            IsActivated = memento.IsActivated;
            IsBlocked = memento.IsBlocked;
            ImageName = memento.ImageName;
            MaliciousActions = memento.MaliciousActions.Select(a => new MaliciousAction(a));
            Validate();
        }

        public void Activate()
        {
            if (IsBlocked)
                throw new ValidationException("Activation isn't possible because the patient is blocked.");
            if (IsActivated)
                throw new ValidationException("Patient account is already activated.");

            IsActivated = true;
        }

        public void Block()
        {
            if (!IsActivated)
                throw new ValidationException("Blocking unactivated patient account isn't possible.");
            if (IsBlocked)
                throw new ValidationException("Patient account is already blocked.");
            if (!IsMalicious())
                throw new ValidationException("Patient isn't malicious.");

            IsBlocked = true;
        }

        public bool IsMalicious()
        {
            if (MaliciousActions.Count() >= 3)
                return true;

            return false;
        }

        public PatientAccountMemento GetPatientMemento()
        {
            return new PatientAccountMemento()
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
                IsBlocked = IsBlocked,
                IsActivated = IsActivated,
                ImageName = ImageName,
                MaliciousActions = MaliciousActions.Select(a => a.GetMemento())
            };
        }

        public override UserAccountMemento GetMemento()
        {
            return GetPatientMemento();
        }

        protected override void Validate()
        {
            base.Validate();

            if (string.IsNullOrWhiteSpace(ImageName))
                throw new ValidationException("Image name cannot be empty.");
        }
    }
}
