﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.DTO
{
    public class DoctorDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Jmbg { get; set; }

        public DoctorDTO() { }

        public DoctorDTO(string name, string surname, string jmbg)
        {
            Name = name;
            Surname = surname;
            Jmbg = jmbg;
        }
    }
}
