using PatientService.Model.Specification;

namespace PatientService.Model.TherapySpecification
{
    public class TherapyDoctorSurnameSpecification : CompositeSpecification<Therapy>
    {
        private string _doctorSurname;

        public TherapyDoctorSurnameSpecification(string doctorSurname)
        {
            _doctorSurname = doctorSurname;
        }

        public override bool IsSatisfiedBy(Therapy o)
        {
            if (_doctorSurname is null)
                return true;

            return o.Doctor.Surname.ToLower().Contains(_doctorSurname.ToLower());
        }
    }
}
