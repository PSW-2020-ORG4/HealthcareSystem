using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.PerformingExamination;

namespace Backend.Service.SearchSpecification.TherapySearch
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

            return DateTime.Compare(o.StartDate, (DateTime)_endDate) <= 0;
        }
    }
}
