using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.PerformingExamination;

namespace Backend.Service.SearchSpecification.ExaminationSearch
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
