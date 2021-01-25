using System;

namespace IntegrationAdapters.Dtos
{
    public class DateRangeDTO
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public DateRangeDTO()
        {
            From = DateTime.Now;
            To = DateTime.Now;
        }
    }
}
