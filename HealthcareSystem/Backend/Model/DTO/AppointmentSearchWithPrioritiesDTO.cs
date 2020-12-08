using Backend.Model.Enums;

namespace Backend.Model.DTO
{
    public class AppointmentSearchWithPrioritiesDTO
    {
        public BasicAppointmentSearchDTO InitialParameters { get; set; }
        public SearchPriority Priority { get; set; }
        public int SpecialtyId { get; set; }

        public void IsAppointmentValid()
        {
            InitialParameters.IsAppointmentValid();
        }
    }
}
