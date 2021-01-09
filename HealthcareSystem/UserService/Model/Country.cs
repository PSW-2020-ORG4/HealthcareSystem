using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model.Memento;
using UserService.CustomException;

namespace UserService.Model
{
    public class Country : IOriginator<CountryMemento>
    {
        private int Id { get; }
        private string Name { get; }

        public Country(int id, string name)
        {
            Id = id;
            Name = name;
            Validate();
        }

        public Country(CountryMemento memento)
        {
            Id = memento.Id;
            Name = memento.Name;
            Validate();
        }

        public CountryMemento GetMemento()
        {
            return new CountryMemento()
            {
                Id = Id,
                Name = Name
            };
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ValidationException("County name cannot be empty.");
        }
    }
}
