using Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
