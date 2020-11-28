using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.PerformingExamination;

namespace Backend.Service.SearchSpecification.TherapySearch
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

            return o.Examination.Doctor.Surname.ToLower().Contains(_doctorSurname.ToLower());
        }
    }
}
