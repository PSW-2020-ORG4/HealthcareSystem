using System;

namespace UserService.Model.Memento
{
    public class UserAccountMemento : IMemento
    {
        public string Jmbg { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string HomeAddress { get; set; }
        public CityMemento City { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
