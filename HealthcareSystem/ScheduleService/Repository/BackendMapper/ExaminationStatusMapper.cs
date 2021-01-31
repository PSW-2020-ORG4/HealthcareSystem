using Backend.Model.Enums;

namespace ScheduleService.Repository.BackendMapper
{
    internal static class ExaminationStatusMapper
    {
        internal static Model.ExaminationStatus ToExaminationStatus(this ExaminationStatus status)
        {
            switch (status)
            {
                case ExaminationStatus.CREATED:
                    return Model.ExaminationStatus.Created;
                case ExaminationStatus.CANCELED:
                    return Model.ExaminationStatus.Canceled;
                default:
                    return Model.ExaminationStatus.Finished;
            }
        }

        internal static ExaminationStatus ToBackendExaminationStatus(this Model.ExaminationStatus status)
        {
            switch (status)
            {
                case Model.ExaminationStatus.Created:
                    return ExaminationStatus.CREATED;
                case Model.ExaminationStatus.Canceled:
                    return ExaminationStatus.CANCELED;
                default:
                    return ExaminationStatus.FINISHED;
            }
        }
    }
}
