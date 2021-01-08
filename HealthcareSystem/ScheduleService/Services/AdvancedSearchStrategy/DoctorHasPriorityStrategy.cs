using ScheduleService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Services.AdvancedSearchStrategy
{
    public class DoctorHasPriorityStrategy : IAdvancedSearchStrategy
    {
        private BasicSearchDTO SearchDTO { get; }

        private int _dateInterval;

        public DoctorHasPriorityStrategy(BasicSearchDTO searchDTO, int dateInterval)
        {
            SearchDTO = searchDTO;
            _dateInterval = dateInterval;
        }

        public BasicSearchDTO GetSearchParameters()
        {
            if (_dateInterval != 0)
            {
                SetupEarliestDate();
                SetupLatestDate();
                _dateInterval--;
                return SearchDTO;
            }
            return null;
        }

        private void SetupEarliestDate()
        {
            DateTime earliestDateTime = SearchDTO.EarliestDateTime.AddDays(-1);
            SearchDTO.EarliestDateTime = GetFixedEarliestDateTime(earliestDateTime);
        }

        private DateTime GetFixedEarliestDateTime(DateTime earliestDateTime)
        {
            if (CheckIfDatePassed(earliestDateTime))
                return DateTime.Now.AddDays(1);
            return earliestDateTime;
        }

        private bool CheckIfDatePassed(DateTime earliestDateTime)
        {
            return earliestDateTime.CompareTo(DateTime.Now) <= 0 ? true : false;
        }

        private void SetupLatestDate()
        {
            SearchDTO.LatestDateTime = SearchDTO.LatestDateTime.AddDays(+1);
        }

    }
}
