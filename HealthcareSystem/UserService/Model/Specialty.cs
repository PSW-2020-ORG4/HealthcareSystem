using System;
using UserService.Model.Memento;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.CustomException;

namespace UserService.Model
{
    public class Specialty : IOriginator<SpecialtyMemento>
    {
        private int Id { get; set; }
        private string Name { get; set; }

        public Specialty(int id, string name)
        {
            Id = id;
            Name = name;
            Validate();
        }

        public Specialty(SpecialtyMemento memento)
        {
            Restore(memento);
        }

        public SpecialtyMemento GetMemento()
        {
            return new SpecialtyMemento
            {
                Id = Id,
                Name = Name
            };
        }

        public void Restore(SpecialtyMemento memento)
        {
            Id = memento.Id;
            Name = memento.Name;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Name)) throw new ValidationException("Specialty name can't be empty!");
        }
    }
}
