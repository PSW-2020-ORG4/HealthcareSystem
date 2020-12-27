using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Model
{
    public class Country
    {
        private int Id { get; }
        private string Name { get; }

        public Country(int id, string name)
        {
            Id = id;
            Name = name;
            Validate();
        }

        private void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
