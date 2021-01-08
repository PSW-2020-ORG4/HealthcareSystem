using ScheduleService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Repository
{
    public interface IPatientRepository
    {
        Patient Get(string id);
        Patient Get(string id, DateTime startDate, DateTime endDate);
    }
}
