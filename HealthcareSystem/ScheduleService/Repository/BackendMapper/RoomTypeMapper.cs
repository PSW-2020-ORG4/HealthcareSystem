using ScheduleService.Model;

namespace ScheduleService.Repository.BackendMapper
{
    internal static class RoomTypeMapper
    {
        internal static RoomType ToRoomType(this Backend.Model.Enums.TypeOfUsage type)
        {
            switch (type)
            {
                case Backend.Model.Enums.TypeOfUsage.CONSULTING_ROOM:
                    return RoomType.Examination;
                case Backend.Model.Enums.TypeOfUsage.OPERATION_ROOM:
                    return RoomType.Surgery;
                default:
                    return RoomType.Hospitalization;
            }
        }

        internal static Backend.Model.Enums.TypeOfUsage ToBackendRoomType(this RoomType type)
        {
            switch (type)
            {
                case RoomType.Examination:
                    return Backend.Model.Enums.TypeOfUsage.CONSULTING_ROOM;
                case RoomType.Surgery:
                    return Backend.Model.Enums.TypeOfUsage.OPERATION_ROOM;
                default:
                    return Backend.Model.Enums.TypeOfUsage.SICKROOM;
            }
        }
    }
}
