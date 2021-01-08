using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.DTOs
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Jmbg { get; set; }
        public string Type { get; set; }
        public bool CanLogIn { get; set; }
    }
}
