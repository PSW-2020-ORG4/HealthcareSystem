using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.CustomException;

namespace UserService.Model
{
    public class City
    {
        private int Id { get; }
        private string Name { get; }
        private Country Country { get; }

        public City(int id, string name, int countryId, string countryName)
        {
            Id = id;
            Name = name;
            Country = new Country(id, name);
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Name)) throw new ValidationException("City name can't be empty!");
        }
    }
}
