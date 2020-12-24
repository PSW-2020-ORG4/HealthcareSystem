using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.DTOs
{
    public class LoggedUserDTO
    {
        public string Username { get; set; }
        public string Jmbg { get; set; }
        public string Role { get; set; }
        public LoggedUserDTO(string username, string jmbg, string role)
        {
            Username = username;
            Jmbg = jmbg;
            Role = role;
        }
    }
}
