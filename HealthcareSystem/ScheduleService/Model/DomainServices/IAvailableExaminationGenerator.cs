using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Model.DomainServices
{
    public interface IAvailableExaminationGenerator
    {
        IEnumerable<Examination> Generate(ExaminationGeneratorDTO examinationDTO);
    }
}
