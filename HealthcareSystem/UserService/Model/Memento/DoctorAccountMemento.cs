using System.Collections.Generic;

namespace UserService.Model.Memento
{
    public class DoctorAccountMemento : UserAccountMemento
    {
        public IEnumerable<SpecialtyMemento> Specialties { get; set; }
    }
}
