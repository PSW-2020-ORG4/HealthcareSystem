using Model.Users;

namespace Backend.Model.Users
{
    public class DoctorSpecialty
    {
        public string DoctorJmbg { get; set; }
        public virtual Doctor Doctor { get; set; }
        public int SpecialtyId { get; set; }
        public virtual Specialty Specialty { get; set; }

        public DoctorSpecialty() { }
        public DoctorSpecialty(string doctorJmbg, int specialityId)
        {
            DoctorJmbg = doctorJmbg;
            SpecialtyId = specialityId;
        }
    }
}
