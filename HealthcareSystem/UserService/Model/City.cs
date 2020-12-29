using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model.Memento;

namespace UserService.Model
{
    public class City : IOriginator<CityMemento>
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

        public City(CityMemento memento)
        {
            Id = memento.Id;
            Name = memento.Name;
            Country = new Country(memento.Country);
            Validate();
        }

        public CityMemento GetMemento()
        {
            return new CityMemento()
            {
                Id = Id,
                Name = Name,
                Country = Country.GetMemento()
            };
        }

        private void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
