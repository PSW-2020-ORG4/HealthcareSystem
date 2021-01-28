using System.Collections.Generic;
using System.Linq;
using UserService.Model.Memento;

namespace UserService.Model
{
    public class DoctorAccount : UserAccount
    {
        private IEnumerable<Specialty> Specialties { get; }

        public DoctorAccount(DoctorAccountMemento memento)
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
    }
}
