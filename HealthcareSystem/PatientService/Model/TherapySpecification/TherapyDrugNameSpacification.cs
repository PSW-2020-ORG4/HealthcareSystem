using PatientService.Model.Specification;

namespace PatientService.Model.TherapySpecification
{
    public class TherapyDrugNameSpacification : CompositeSpecification<Therapy>
    {
        private string _drugName;

        public TherapyDrugNameSpacification(string drugName)
        {
            _drugName = drugName;
        }

        public override bool IsSatisfiedBy(Therapy o)
        {
            if (_drugName is null)
                return true;

            return o.Drug.Name.ToLower().Contains(_drugName.ToLower());
        }
    }
}
