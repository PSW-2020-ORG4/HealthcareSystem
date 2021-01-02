using System;
using UserService.Model.Memento;
using UserService.CustomException;

namespace UserService.Model
{
    public class UserAccount : IOriginator<UserAccountMemento>
    {
        protected Jmbg Jmbg { get; }
        protected string Name { get; }
        protected string Surname { get; }
        protected Gender Gender { get; }
        protected DateTime DateOfBirth { get; }
        protected PhoneNumber Phone { get; }
        protected Address HomeAddress { get; }
        protected City City { get; }
        protected Email Email { get; }
        protected Password Password { get; }
        protected UserType UserType { get; }

        public UserAccount(UserAccountMemento memento)
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
            Validate();
        }

        public virtual UserAccountMemento GetMemento()
        {
            return new UserAccountMemento()
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
                UserType = UserType
            };
        }

        protected virtual void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name)) 
                throw new ValidationException("Name cannot be empty.");
            if (string.IsNullOrWhiteSpace(Surname)) 
                throw new ValidationException("Surname cannot be empty.");
        }
    }
}
