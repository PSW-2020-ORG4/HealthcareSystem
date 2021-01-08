using ScheduleService.Model;
using System;

namespace ScheduleService.Repository
{
    public interface IPatientRepository
    {
        Patient Get(string id);
        Patient Get(string id, DateTime startDate, DateTime endDate);
    }
}
