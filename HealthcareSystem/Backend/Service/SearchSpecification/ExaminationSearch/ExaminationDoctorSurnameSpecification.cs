using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model.PerformingExamination;

namespace Backend.Service.SearchSpecification.ExaminationSearch
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
