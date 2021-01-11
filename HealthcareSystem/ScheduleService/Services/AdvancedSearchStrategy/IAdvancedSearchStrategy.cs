using ScheduleService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Services.AdvancedSearchStrategy
{
    public interface IAdvancedSearchStrategy
    {
        BasicSearchDTO GetSearchParameters();
    }
}
