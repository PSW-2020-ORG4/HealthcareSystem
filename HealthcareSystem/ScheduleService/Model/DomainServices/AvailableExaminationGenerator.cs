using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Model.DomainServices
{
    public class AvailableExaminationGenerator : IAvailableExaminationGenerator
    {
        public IEnumerable<Examination> Generate(ExaminationGeneratorDTO examinationDTO)
        {
            throw new NotImplementedException();
        }
    }
}
