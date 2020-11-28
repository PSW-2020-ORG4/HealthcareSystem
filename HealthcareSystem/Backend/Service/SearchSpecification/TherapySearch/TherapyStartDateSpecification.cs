using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.PerformingExamination;

namespace Backend.Service.SearchSpecification.TherapySearch
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

            return DateTime.Compare(o.StartDate, (DateTime)_startDate) >= 0;
        }
    }
}
