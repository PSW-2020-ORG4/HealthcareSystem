using PatientService.Model.Specification;

namespace PatientService.Model.ExaminationSpecification
{
    public class ExaminationAnamnesisSpecification : CompositeSpecification<Examination>
    {
        private string _anamnesis;

        public ExaminationAnamnesisSpecification(string anamnesis)
        {
            _anamnesis = anamnesis;
        }

        public override bool IsSatisfiedBy(Examination o)
        {
            if (_anamnesis is null)
                return true;

            return o.Anamnesis.ToLower().Contains(_anamnesis.ToLower());
        }
    }
}
