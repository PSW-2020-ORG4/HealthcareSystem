using Backend.Model.Enums;

namespace Backend.Model.DTO
{
    public class AppointmentSearchWithPrioritiesDTO
    {
        public BasicAppointmentSearchDTO InitialParameters { get; set; }
        public SearchPriority Priority { get; set; }
        public int SpecialtyId { get; set; }

        public AppointmentSearchWithPrioritiesDTO(BasicAppointmentSearchDTO initialParameters, SearchPriority priority, int specialtyId)
        {
            InitialParameters = initialParameters;
            Priority = priority;
            SpecialtyId = specialtyId;
        }

        public void IsAppointmentValid()
        {
            InitialParameters.IsAppointmentValid();
        }
    }
}
