using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Model.Memento
{
    public class CityMemento : IMemento
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CountryMemento Country { get; set; }
    }
}
