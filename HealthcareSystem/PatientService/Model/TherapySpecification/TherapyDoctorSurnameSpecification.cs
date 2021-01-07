using PatientService.Model;
using PatientService.Model.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Model.TherapySpecification
{
    public class TherapyDoctorSurnameSpecification : CompositeSpecification<Therapy>
    {
        private string _doctorSurname;

        public TherapyDoctorSurnameSpecification(string doctorSurname)
        {
            _doctorSurname = doctorSurname;
        }

        public override bool IsSatisfiedBy(Therapy o)
        {
            if (_doctorSurname is null)
                return true;

            return o.Doctor.Surname.ToLower().Contains(_doctorSurname.ToLower());
        }
    }
}
