using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Backend.Model.Pharmacies
{
    public class DateRange : ValueObject
    {
        [JsonProperty("from")]
        public DateTime From { get; private set; }
        [JsonProperty("to")]
        public DateTime To { get; private set; }

        public DateRange() { }

        [JsonConstructor]
        public DateRange(DateTime from, DateTime to)
        {
            From = from;
            To = to;
            Validate();
        }

        private void Validate()
        {
            if(From == null || To == null)
                throw new Exceptions.ValidationException("Start date and/or End date is null!");
            if (From.CompareTo(To) > 0)
                throw new Exceptions.ValidationException("Start date should be before End date!");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return From;
            yield return To;
        }
    }
}
