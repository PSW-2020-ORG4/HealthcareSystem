using System;
using System.Collections.Generic;
using System.Linq;
using UserService.Model.Memento;

namespace UserService.Model
{
    public class PatientAccount : UserAccount
    {
        private DateTime DateOfRegistration { get; }
        private bool IsActivated { get; set; }
        private bool IsBlocked { get; set; }
        private string ImageName { get; }
        private IEnumerable<MaliciousAction> MaliciousActions { get; }

        public PatientAccount(PatientAccountMemento memento) : base(memento)
        {
            DateOfRegistration = memento.DateOfRegistration;
            IsActivated = memento.IsActivated;
            IsBlocked = memento.IsBlocked;
            ImageName = memento.ImageName;
            MaliciousActions = memento.MaliciousActions.Select(a => new MaliciousAction(a));
            Validate();
        }

        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Block()
        {
            throw new NotImplementedException();
        }

        public bool IsMalicious()
        {
            throw new NotImplementedException();
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
                DateOfRegistration = DateOfRegistration,
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
            throw new NotImplementedException();
        }
    }
}
