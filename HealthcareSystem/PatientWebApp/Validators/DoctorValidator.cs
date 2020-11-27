using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.Validators
{
    public class DoctorValidator
    {
        public bool IsValidDoctorJmbg(string jmbg)
        {
            if(jmbg != null && jmbg != "") return true;
            return false;
        }

    }
}
