using PatientService.Model.Specification;
using System;

namespace PatientService.Model.TherapySpecification
{
    public class TherapyEndDateSpecification : CompositeSpecification<Therapy>
    {
        private DateTime? _endDate;

        public TherapyEndDateSpecification(DateTime? endDate)
        {
            _endDate = endDate;
        }

        public override bool IsSatisfiedBy(Therapy o)
        {
            if (_endDate is null)
                return true;

            return DateTime.Compare(o.DateRange.StartDate, (DateTime)_endDate) <= 0;
        }
    }
}
