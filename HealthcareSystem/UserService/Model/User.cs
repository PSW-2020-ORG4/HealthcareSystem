using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Model
{
    public class User
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

        public User()
        {
            Validate();
        }

        protected virtual void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
