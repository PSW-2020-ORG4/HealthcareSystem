﻿using System;
using UserService.CustomException;
using UserService.Model.Memento;

namespace UserService.Model
{
    public class UserAccount : IOriginator<UserAccountMemento>
    {
        protected Jmbg Jmbg { get; set; }
        protected string Name { get; set; }
        protected string Surname { get; set; }
        protected Gender Gender { get; set; }
        protected DateTime DateOfBirth { get; set; }
        protected PhoneNumber Phone { get; set; }
        protected Address HomeAddress { get; set; }
        protected City City { get; set; }
        protected Email Email { get; set; }
        protected Password Password { get; set; }
        protected UserType UserType { get; set; }

        protected UserAccount()
        {
        }

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
            UserType = memento.UserType;
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

        public virtual bool CanLogIn()
        {
            return true;
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
