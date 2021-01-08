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

        private int counter;

        public DoctorHasPriorityStrategy(BasicSearchDTO searchDTO)
        {
            SearchDTO = searchDTO;
            counter = 0;
        }

        public BasicSearchDTO GetSearchParameters()
        {
            if (counter == 0)
            {
                counter++;
                return SearchDTO;
            }              
            if (counter > 5) return null;          
            SetupEarliestDate();
            SetupLatestDate();
            counter++;
            return SearchDTO;
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
            return earliestDateTime.CompareTo(DateTime.Now) == -1 ? true : false;
        }

        private void SetupLatestDate()
        {
            SearchDTO.LatestDateTime = SearchDTO.LatestDateTime.AddDays(+1);
        }

    }
}
