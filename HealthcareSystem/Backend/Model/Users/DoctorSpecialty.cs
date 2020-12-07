using System;
using System.Collections.Generic;
using System.Text;
using Model.Users;

namespace Backend.Model.Users
{
    public class DoctorSpecialty
    {
        public string DoctorJmbg { get; set; }
        public virtual Doctor Doctor { get; set; }
        public int SpecialtyId { get; set; }
        public virtual Specialty Specialty { get; set; }
    }
}
