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
            SearchDTO.EarliestDateTime = SearchDTO.EarliestDateTime.AddDays(-1);
        }

        private void SetupLatestDate()
        {
            SearchDTO.LatestDateTime = SearchDTO.LatestDateTime.AddDays(+1);
        }

    }
}
