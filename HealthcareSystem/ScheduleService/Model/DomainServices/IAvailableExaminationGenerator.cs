using System.Collections.Generic;

namespace ScheduleService.Model.DomainServices
{
    public interface IAvailableExaminationGenerator
    {
        IEnumerable<Examination> Generate(ExaminationGeneratorDTO examinationDTO);
    }
}
