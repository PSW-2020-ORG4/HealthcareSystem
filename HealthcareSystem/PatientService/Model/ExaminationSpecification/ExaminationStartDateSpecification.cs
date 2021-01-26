using PatientService.Model.Specification;
using System;

namespace PatientService.Model.ExaminationSpecification
{
    public class ExaminationStartDateSpecification : CompositeSpecification<Examination>
    {
        private DateTime? _startDate;

        public ExaminationStartDateSpecification(DateTime? startDate)
        {
            _startDate = startDate;
        }

        public override bool IsSatisfiedBy(Examination o)
        {
            if (_startDate is null)
                return true;

            return DateTime.Compare(o.DateAndTime, (DateTime)_startDate) >= 0;
        }
    }
}
