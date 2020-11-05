using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.DTOs
{
    public class PatientDTO
    {
        public string Jmbg { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public PatientDTO() { }
    }
}
