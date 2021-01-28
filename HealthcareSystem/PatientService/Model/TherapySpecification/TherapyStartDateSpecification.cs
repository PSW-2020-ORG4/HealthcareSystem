using PatientService.Model.Specification;
using System;

namespace PatientService.Model.TherapySpecification
{
    public class TherapyStartDateSpecification : CompositeSpecification<Therapy>
    {
        private DateTime? _startDate;

        public TherapyStartDateSpecification(DateTime? startDate)
        {
            _startDate = startDate;
        }

        public override bool IsSatisfiedBy(Therapy o)
        {
            if (_startDate is null)
                return true;

            return DateTime.Compare(o.DateRange.StartDate, (DateTime)_startDate) >= 0;
        }
    }
}
