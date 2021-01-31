using PatientService.CustomException;
using System;

namespace PatientService.Model
{
    public class DateRange
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            Validate();
        }

        public override bool Equals(object obj)
        {
            return obj is DateRange range &&
                   StartDate == range.StartDate &&
                   EndDate == range.EndDate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StartDate, EndDate);
        }

        private void Validate()
        {
            if (EndDate < StartDate)
                throw new ValidationException("Start date must be earlier than the end date.");
        }
    }
}
