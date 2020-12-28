using Backend.Model.DTO;
using Model.PerformingExamination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Service.ExaminationAndPatientCard
{
    public interface IFreeAppointmentSearchService
    {
        ICollection<Examination> BasicSearch(BasicAppointmentSearchDTO parameters);
        ICollection<Examination> SearchWithPriorities(AppointmentSearchWithPrioritiesDTO parameters);
        //ICollection<Examination> GetUnchangedAppointmentsForEmergency(BasicAppointmentSearchDTO parameters);
        //ICollection<Examination> GetShiftedAndSortedAppoinmentsForEmergency(BasicAppointmentSearchDTO parameters);
    }
}
