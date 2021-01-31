using GraphicalEditorServer.DTO;
using Model.Manager;

namespace GraphicalEditorServer.Mappers
{
    public class RenovationPeriodMapper
    {
        public static RenovationPeriodDTO RenovationPeriodToRenovationPeriodDTO(RenovationPeriod renovationPeriod)
        {
            return new RenovationPeriodDTO(renovationPeriod.BeginDate, renovationPeriod.EndDate);
        }
    }
}
