using Backend.Model.DTO;
using Backend.Model.PerformingExamination;
using System.Collections.Generic;

namespace Backend.Service.ExaminationAndPatientCard
{
    public interface IFreeAppointmentSearchService
    {
        ICollection<Examination> BasicSearch(BasicAppointmentSearchDTO parameters);
        ICollection<Examination> SearchWithPriorities(AppointmentSearchWithPrioritiesDTO parameters);
        ICollection<Examination> GetUnchangedAppointmentsForEmergency(AppointmentSearchWithPrioritiesDTO parameters);
        ICollection<Examination> GetShiftedAndSortedAppoinmentsForEmergency(AppointmentSearchWithPrioritiesDTO parameters);
        List<Examination> GetOnlyAdequateAppointmentsForEmergency(AppointmentSearchWithPrioritiesDTO parameters);
        void SetNewDateTimesForEmergency(BasicAppointmentSearchDTO parameters);
    }
}
