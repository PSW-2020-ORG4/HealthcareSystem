using Backend.Model.Enums;
using Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class Survey
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Grade PolitenessGrade { get; set; }
        public Grade ProfessionalismGrade { get; set; }
        public Grade StaffGrade { get; set; }

        [ForeignKey("Doctor")]
        public string DoctorJmbg { get; set; }
        public virtual Doctor Doctor { get; set; }

        public Survey() { }

        public Survey(int id, Grade politenessGrade, Grade professionalismGrade, Grade staffGrade, Doctor doctor)
        {
            Id = id;
            PolitenessGrade = politenessGrade;
            ProfessionalismGrade = professionalismGrade;
            StaffGrade = staffGrade;
            if (doctor != null)
            {
                Doctor = new Doctor(doctor);
                DoctorJmbg = doctor.Jmbg;
            }
            else
            {
                Doctor = new Doctor();
                DoctorJmbg = null;
            }
        }
    }
}
