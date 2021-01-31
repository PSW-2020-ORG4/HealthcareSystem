using Backend.Model.Enums;
using ScheduleService.Model;

namespace ScheduleService.Repository.BackendMapper
{
    internal static class ExaminationTypeMapper
    {
        internal static ExaminationType ToExaminationType(this TypeOfExamination type)
        {
            switch (type)
            {
                case TypeOfExamination.GENERAL:
                    return ExaminationType.Examination;
                default:
                    return ExaminationType.Surgery;
            }
        }

        internal static TypeOfExamination ToBackendExaminationType(this ExaminationType type)
        {
            switch (type)
            {
                case ExaminationType.Examination:
                    return TypeOfExamination.GENERAL;
                default:
                    return TypeOfExamination.SURGERY;
            }
        }
    }
}
