using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp
{
    public interface IJWTAuthenticationManager
    {
        string Authenticate(string username, string jmbg, string role);
        LoggedUserDTO GetLoggedUser(string token);
    }
}
