using ScheduleService.Model;
using System;
using System.Collections.Generic;

namespace ScheduleService.Repository
{
    public interface IDoctorRepository
    {
        Doctor Get(string id);
        Doctor Get(string id, DateTime startDate, DateTime endDate);
        ICollection<string> GetIdsBySpecialty(int specialtyId);
    }
}
