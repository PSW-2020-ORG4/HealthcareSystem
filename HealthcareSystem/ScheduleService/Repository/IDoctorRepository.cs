using ScheduleService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Repository
{
    public interface IDoctorRepository
    {
        Doctor Get(string id);
        Doctor Get(string id, DateTime startDate, DateTime endDate);
        IEnumerable<Doctor> GetBySpecialty(int specialtyId, DateTime startDate, DateTime endDate);
    }
}
