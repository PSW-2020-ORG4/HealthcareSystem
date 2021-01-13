using PatientService.Model.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Model.ExaminationSpecification
{
    public class ExaminationDoctorSurnameSpecification : CompositeSpecification<Examination>
    {
        private string _doctorSurname;

        public ExaminationDoctorSurnameSpecification(string doctorSurname)
        {
            _doctorSurname = doctorSurname;
        }

        public override bool IsSatisfiedBy(Examination o)
        {
            if (_doctorSurname is null)
                return true;

            return o.Doctor.Surname.ToLower().Contains(_doctorSurname.ToLower());
        }
    }
}
