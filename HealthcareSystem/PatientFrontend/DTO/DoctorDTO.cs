using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKorporacija.DTO
{
    class DoctorDTO
    {
        public string Jmbg { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public DoctorDTO(string jmbg, string name, string surname)
        {
            this.Jmbg = jmbg;
            this.Name = name;
            this.Surname = surname;
        }

        public override string ToString()
        {
            return  Name + " " + Surname + " " + Jmbg;
        }
    }
}
