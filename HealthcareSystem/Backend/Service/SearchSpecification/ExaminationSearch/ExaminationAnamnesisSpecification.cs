using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.PerformingExamination;

namespace Backend.Service.SearchSpecification.ExaminationSearch
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
