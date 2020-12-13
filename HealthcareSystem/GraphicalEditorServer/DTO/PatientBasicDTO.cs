using Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.DTO
{
    public class PatientBasicDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Jmbg { get; set; }
        public int PatientCardId { get; set; }

        public PatientBasicDTO() { }

        public PatientBasicDTO(string name, string surname, string jmbg, int patientCardId)
        {
            Name = name;
            Surname = surname;
            Jmbg = jmbg;
            PatientCardId = patientCardId;
        }
    }
}
