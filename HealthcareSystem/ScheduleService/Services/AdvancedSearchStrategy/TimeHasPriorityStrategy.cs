using ScheduleService.DTO;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleService.Services.AdvancedSearchStrategy
{
    public class TimeHasPriorityStrategy : IAdvancedSearchStrategy
    {
        private BasicSearchDTO SearchDTO { get; }
        private ICollection<string> PotentiallyAvailableDoctors { get; }

        public TimeHasPriorityStrategy(BasicSearchDTO searchDTO, ICollection<string> potentiallyAvailableDoctors)
        {
            SearchDTO = searchDTO;
            PotentiallyAvailableDoctors = potentiallyAvailableDoctors;
        }

        public BasicSearchDTO GetSearchParameters()
        {
            if (PotentiallyAvailableDoctors.Count > 0)
            {
                SearchDTO.DoctorJmbg = PotentiallyAvailableDoctors.FirstOrDefault();
                PotentiallyAvailableDoctors.Remove(SearchDTO.DoctorJmbg);
                return SearchDTO;
            }
            return null;
        }

    }
}
