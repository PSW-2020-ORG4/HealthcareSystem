using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model.Pharmacies
{
    public class DateRange
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public DateRange(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }

        public DateRange()
        {
            From = new DateTime();
            To = new DateTime();
        }
    }
}
