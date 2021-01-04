using PatientService.Model.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Model.ExaminationSpecification
{
    public class ExaminationEndDateSpecification : CompositeSpecification<Examination>
    {
        private DateTime? _endDate;

        public ExaminationEndDateSpecification(DateTime? endDate)
        {
            _endDate = endDate;
        }

        public override bool IsSatisfiedBy(Examination o)
        {
            if (_endDate is null)
                return true;

            return DateTime.Compare(o.DateAndTime, (DateTime)_endDate) <= 0;
        }
    }
}
