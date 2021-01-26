using Model.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend
{
    public class Country
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<City> Cities { get; set; }

        public Country() { }

        public Country(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public Country(Country country)
        {
            Id = country.Id;
            Name = country.Name;
        }
    }
}
